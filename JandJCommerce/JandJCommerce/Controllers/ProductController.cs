using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    [Authorize(Policy ="AdminOnly")]
    public class ProductController : Controller
    {
        private IInventory _inventory;

        public ProductController(IInventory inventory)
        {
            _inventory = inventory;
        }


        [HttpGet(Name ="Details")]
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _inventory.GetProductById(id);
            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Product product = await _inventory.GetProductById(id);
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product)
        {
            string result = await _inventory.UpdateProduct(product.ID, product);

            if (result == "Product Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Admin");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string result = await _inventory.DeleteProduct(id);


            if (result == "Product Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Admin");
        }


        [HttpGet(Name ="Create")]
        public IActionResult Create()
        {
            return View(new Product());
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _inventory.CreateProduct(product);

            if (result == "Product Not Created")
            {
                return View(product);
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}