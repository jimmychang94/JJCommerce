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
    /// <summary>
    /// This controller edits the products on our site so only admins can access it
    /// </summary>
    [Authorize(Policy ="AdminOnly")]
    public class ProductController : Controller
    {
        private IInventory _inventory;

        public ProductController(IInventory inventory)
        {
            _inventory = inventory;
        }

        /// <summary>
        /// This gets the details of that specific product. 
        /// It holds all the information that one might have for it so it is admin only
        /// </summary>
        /// <param name="id">The id of the specific product</param>
        /// <returns>The details view</returns>
        [HttpGet(Name ="Details")]
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _inventory.GetProductById(id);
            return View(product);
        }

        /// <summary>
        /// This gets the product associated with the id and then sends them to the update page
        /// </summary>
        /// <param name="id">The id of the specific product</param>
        /// <returns>The update view</returns>
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Product product = await _inventory.GetProductById(id);
            return View(product);
        }

        /// <summary>
        /// This takes what was changed in the update form and then edits the corresponding product.
        /// It then sends the user back to the admin dashboard.
        /// </summary>
        /// <param name="id">The id of the product to edit</param>
        /// <param name="product">How the product should be edited to</param>
        /// <returns>To the Admin Dashboard</returns>
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

        /// <summary>
        /// This deletes the product associated with the id given.
        /// </summary>
        /// <param name="id">The id of the product to delete</param>
        /// <returns>The admin dashboard</returns>
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

        /// <summary>
        /// This sends the user to the create page for products
        /// </summary>
        /// <returns>The create view page</returns>
        [HttpGet(Name ="Create")]
        public IActionResult Create()
        {
            return View(new Product());
        }

        /// <summary>
        /// This creates the product that has been filled out in the create form.
        /// </summary>
        /// <param name="product">The product to be created</param>
        /// <returns>To the Admin Dashboard</returns>
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