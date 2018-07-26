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

        public async Task<IActionResult> Index()
        {
            AdminViewModel avm = new AdminViewModel();
            avm.Products = await _inventory.GetProducts();
            avm.Orders = await _order.GetOrders();
            return View(avm);
        }
    }
}
