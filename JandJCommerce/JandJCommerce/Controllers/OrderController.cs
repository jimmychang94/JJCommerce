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
    [Authorize(Policy="MemberOnly")]
    public class OrderController : Controller
    {
        private IOrder _order;
        private IBasketItem _item;
        private IInventory _inventory;

        public OrderController(IOrder order, IBasketItem item, IInventory inventory)
        {
            _order = order;
            _item = item;
            _inventory = inventory;
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _order.GetOrderById(id);
            order.BasketItems = await _item.GetBasketItems(order.BasketID);
            foreach (BasketItem item in order.BasketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
            }
            return View(order);
        }

        [Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _order.DeleteOrder(id);
            return RedirectToAction("Index", "Admin");
            
        }
    }
}
