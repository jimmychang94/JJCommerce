using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    public class HomeController : Controller
    {
        private IInventory _inventory;
        private UserManager<ApplicationUser> _userManager;

        public IConfiguration Configuration { get; }

        public HomeController(IInventory inventory, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _inventory = inventory;
            _userManager = userManager;
            Configuration = configuration;
        }

        public IActionResult Index ()
        {
            return View();
        }

        [Authorize(Policy = "Seattle")]
        public IActionResult Seattle()
        {
            return View();
        }

        [Authorize(Policy = "Cat")]
        public async Task<IActionResult> Cat()
        {
            EmailSender emailSender = new EmailSender(Configuration);
            string htmlMessage = "<p>Cats are Awesome!</p> <p>We can be cute, mean, <br />And anywhere inbetween. <br /> While we are around <br /> we turn your frown <br /> upside down. <br /> But when you want us <br /> you can't find us <br /> We just can't be found. <br /> - Catz</p>";
            await emailSender.SendCatmailAsync(User.Identity.Name, "Catz", htmlMessage);
            return View();
        }
    }
}
