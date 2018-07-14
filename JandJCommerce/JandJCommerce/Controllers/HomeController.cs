using JandJCommerce.Models.Interfaces;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(IInventory inventory)
        {
            _inventory = inventory;
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
        public IActionResult Cat()
        {
            return View();
        }
    }
}
