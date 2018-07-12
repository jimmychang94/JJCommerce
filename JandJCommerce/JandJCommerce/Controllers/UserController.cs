using System;
using System.Collections.Generic;
using System.Linq;
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
                    await _signInManager.SignInAsync(user, isPersistent: true);
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
    }
}