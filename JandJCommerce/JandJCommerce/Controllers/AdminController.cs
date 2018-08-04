using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    // This authorize policy makes sure that only admins can access this controller and all the pages attached to it unless another policy is declared.
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private IInventory _inventory;
        private IOrder _order;

        public AdminController(IInventory inventory, IOrder order)
        {
            _inventory = inventory;
            _order = order;
        }

        /// <summary>
        /// This shows the default page for the admin controller which is the admin dashboard.
        /// </summary>
        /// <returns>The view of the admin dashboard</returns>
        public async Task<IActionResult> Index()
        {
            // The Admin View Model is used to store both the list of products and the list of the last 20 orders so that we can display them both on one page.
            AdminViewModel avm = new AdminViewModel();
            avm.Products = await _inventory.GetProducts();
            avm.Orders = await _order.GetOrders();
            return View(avm);
        }
    }
}
