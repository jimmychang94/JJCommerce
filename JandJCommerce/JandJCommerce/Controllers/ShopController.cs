using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    /// <summary>
    /// This page is open for anyone to view
    /// </summary>
    public class ShopController : Controller
    {
        private IInventory _inventory;

        public ShopController(IInventory inventory)
        {
            _inventory = inventory;
        }

        /// <summary>
        /// This shows all the products in our database
        /// </summary>
        /// <returns>This is the product view</returns>
        public IActionResult Index()
        {
            List<Product> allProducts = _inventory.GetProducts().Result;
            return View(allProducts);
        }

        /// <summary>
        /// This shows the details of the specific product
        /// </summary>
        /// <param name="id">The id of the product to view</param>
        /// <returns>The view of the product</returns>
        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _inventory.GetProductById(id);
            return View(product);
        }
    }
}
