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
    /// <summary>
    /// This is the login and register for the user. 
    /// It also holds the ability to login through a 3rd party OAuth. It is open to everyone.
    /// </summary>
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        /// This sends the user to the register page
        /// </summary>
        /// <returns>the register view page</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        /// <summary>
        /// This takes what put into the registration and creates a user.
        /// It uses that information to create claims for the user.
        /// It sends an email to thank the user for registering for our site.
        /// It then logs the user into the site and redirects them to the index page
        /// </summary>
        /// <param name="rvm">The information to be put into the new user</param>
        /// <returns>The home page</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if(ModelState.IsValid)
            {
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

        /// <summary>
        /// This shows the login page
        /// </summary>
        /// <returns>The login page</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        /// <summary>
        /// This takes the information that was put in and checks to see if the information is valid
        /// If it is the user is logged in and sent to the home page
        /// If it isn't they are sent back to the login page
        /// </summary>
        /// <param name="lvm">The login information</param>
        /// <returns>The home page or the login page</returns>
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

        /// <summary>
        /// This logs the user out and redirects them to the home page.
        /// </summary>
        /// <returns>The home page</returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["LoggedOut"] = "User Logged Out";

            return RedirectToAction("Index", "Home");
        }

       /// <summary>
       /// This adds the option to login through an external provider.
       /// </summary>
       /// <param name="provider"></param>
       /// <returns></returns>
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "User");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        /// <summary>
        /// This first checks to see if there is an error
        /// Then it checks to see if the external provider gave information back on the user
        /// Next we check to see if the user has an account with us
        /// If they do, we sign them in, otherwise they get sent to our login page to fill out some extra information.
        /// </summary>
        /// <param name="remoteError"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This is where the user is sent if they created a new account from the external login.
        /// It also adds claims, sends them an email, and logs them in
        /// </summary>
        /// <param name="elvm">This is the extra information we grabbed from the external login</param>
        /// <returns>The home page</returns>
        [HttpPost]
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