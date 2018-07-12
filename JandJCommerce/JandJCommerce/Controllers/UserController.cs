using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                    //phone?
                };

                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    //start making claims to add to list//
                    Claim fullNameClaim = new Claim(ClaimTypes.Name, $"{rvm.FirstName} {rvm.LastName}");
                    Claim emailClaim = new Claim(ClaimTypes.Email, rvm.Email);
                    Claim locationClaim = new Claim(ClaimTypes.StreetAddress, rvm.Location);

                    //add claims to list -claims
                    claims.Add(fullNameClaim);
                    claims.Add(emailClaim);
                    claims.Add(locationClaim);

                    //add all claims /w s for all/ to user manager DB
                    await _userManager.AddClaimsAsync(user, userClaims);


                    await _signInManager.SignInAsync(user, isPersistent: true);
                    IndexUserViewModel iuvm = new IndexUserViewModel();
                    foreach(Claim claim in userClaims)
                    {
                        if(claim.Type == ClaimTypes.Name)
                        {
                            iuvm.LoggedIn = true;
                            iuvm.UserName = claim.Value;
                        }
                    }
                    return RedirectToAction("Index", "Home", iuvm);
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
                    ApplicationUser user = _userManager.Users.FirstOrDefault(u => u.Email == lvm.Email);
                    IEnumerable<Claim> userClaims = await _userManager.GetClaimsAsync(user);
                    IndexUserViewModel iuvm = new IndexUserViewModel()
                    {
                        //MyClaims = myClaims
                    };
                    foreach(Claim claim in userClaims)
                    {
                        if (claim.Type == ClaimTypes.Name)
                        {
                            iuvm.LoggedIn = true;
                            iuvm.UserName = claim.Value;
                        }
                    }
                    return RedirectToAction("Index", "Home", iuvm);
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
    }
}