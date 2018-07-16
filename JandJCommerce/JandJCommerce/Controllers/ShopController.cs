using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    public class ShopController : Controller
    {
        private IInventory _inventory;

        public ShopController(IInventory inventory)
        {
            _inventory = inventory;
        }

        public IActionResult Index()
        {
            List<Product> allProducts = _inventory.GetProducts().Result;
            return View(allProducts);
        }

        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _inventory.GetProductById(id);
            return View(product);
        }
    }
}
