using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private IInventory _inventory;

        public AdminController(IInventory inventory)
        {
            _inventory = inventory;
        }

        public IActionResult Index()
        {
            List<Product> allProducts = _inventory.GetProducts().Result;
            return View(allProducts);
        }
    }
}
