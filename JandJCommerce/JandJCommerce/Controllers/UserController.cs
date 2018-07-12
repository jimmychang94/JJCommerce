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
                List<Claim> myClaims = new List<Claim>();
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
                    Claim nameClaim = new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}");

                    myClaims.Add(nameClaim);
                    await _userManager.AddClaimsAsync(user, myClaims);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    IEnumerable<Claim> myEnumerableClaims = await _userManager.GetClaimsAsync(user);
                    return RedirectToAction("Index", "Home", myEnumerableClaims);
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
                    IEnumerable<Claim> myClaims = await _userManager.GetClaimsAsync(user);
                    IndexUserViewModel indexUser = new IndexUserViewModel()
                    {
                        //MyClaims = myClaims
                    };
                    foreach(Claim claim in myClaims)
                    {
                        if (claim.Type == ClaimTypes.Name)
                        {
                            indexUser.LoggedIn = true;
                            indexUser.UserName = claim.Value;
                        }
                    }
                    return RedirectToAction("Index", "Home", indexUser);
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