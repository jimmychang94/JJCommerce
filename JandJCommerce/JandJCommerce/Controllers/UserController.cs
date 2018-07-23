using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if(ModelState.IsValid)
            {
                //make list of claims to then add too later
                List<Claim> userClaims = new List<Claim>();

                var user = new ApplicationUser
                {
                    UserName = rvm.Email,
                    Email = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    Location = rvm.Location
                };

                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    //start making claims to add to list//
                    Claim fullNameClaim = new Claim("FullName" , $"{rvm.FirstName} {rvm.LastName}");
                    Claim emailClaim = new Claim(ClaimTypes.Email, rvm.Email);
                    Claim locationClaim = new Claim(ClaimTypes.StreetAddress, rvm.Location);

                    //add claims to list -claims
                    userClaims.Add(fullNameClaim);
                    userClaims.Add(emailClaim);
                    userClaims.Add(locationClaim);

                    //add all claims /w s for all/ to user manager DB
                    await _userManager.AddClaimsAsync(user, userClaims);

                    if ((user.Email == "furnitureAdmin@JJfurniture.com") || (user.Email == "amanda@codefellows.com"))
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    }

                    await _signInManager.SignInAsync(user, false);

                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    await _emailSender.SendEmailAsync(user.Email, "Welcome to JandJCommerce!", "<p>Thank you for becoming a member of JandJCommerce!</p>");

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(rvm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync
                    (
                    lvm.Email,
                    lvm.Password,
                    false,
                    lockoutOnFailure: false
                    );

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(lvm.Email);
                   
                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(lvm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["LoggedOut"] = "User Logged Out";

            return RedirectToAction("Index", "Home");
        }

       
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "User");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var results = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey,
                isPersistent: false, bypassTwoFactor: true);

            if (results.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
        }


        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel elvm)
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();

                var user = new ApplicationUser
                {
                    UserName = elvm.Email,
                    Email = elvm.Email,
                    FirstName = elvm.FirstName,
                    LastName = elvm.LastName,
                    Location = elvm.Location
                };

                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    List<Claim> userClaims = new List<Claim>();

                    //start making claims to add to list//
                    Claim fullNameClaim = new Claim("FullName", $"{elvm.FirstName} {elvm.LastName}");
                    Claim emailClaim = new Claim(ClaimTypes.Email, elvm.Email);
                    Claim locationClaim = new Claim(ClaimTypes.StreetAddress, elvm.Location);

                    //add claims to list -claims
                    userClaims.Add(fullNameClaim);
                    userClaims.Add(emailClaim);
                    userClaims.Add(locationClaim);

                    //add all claims /w s for all/ to user manager DB
                    await _userManager.AddClaimsAsync(user, userClaims);

                    if ((user.Email == "furnitureAdmin@JJfurniture.com") || (user.Email == "amanda@codefellows.com"))
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    }

                    result = await _userManager.AddLoginAsync(user, info);



                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);


                        if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                        await _emailSender.SendEmailAsync(user.Email, "Welcome to JandJCommerce!", "<p>Thank you for becoming a member of JandJCommerce!</p>");

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(elvm);
        }
    }
}