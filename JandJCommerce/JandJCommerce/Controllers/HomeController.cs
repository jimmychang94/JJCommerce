using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
