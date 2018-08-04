using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    /// <summary>
    /// This maeks sure that only members can edit orders.
    /// </summary>
    [Authorize(Policy="MemberOnly")]
    public class OrderController : Controller
    {
        private IOrder _order;
        private IBasketItem _item;
        private IInventory _inventory;
        private UserManager<ApplicationUser> _userManager;

        public OrderController(IOrder order, IBasketItem item, IInventory inventory, UserManager<ApplicationUser> userManager)
        {
            _order = order;
            _item = item;
            _inventory = inventory;
            _userManager = userManager;
        }

        /// <summary>
        /// This shows the member specific order. Some information isn't shown because it is sensitive data.
        /// </summary>
        /// <param name="id">The id of the order to view</param>
        /// <returns>The view of the order</returns>
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var order = await _order.GetOrderById(id);
            if (order.UserID != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            order.BasketItems = await _item.GetBasketItems(order.BasketID);
            foreach (BasketItem item in order.BasketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
            }
            return View(order);
        }

        /// <summary>
        /// This is the admin view of the order details.
        /// There is all the information on the specific order that is stored.
        /// </summary>
        /// <param name="id">The id of the order to view</param>
        /// <returns>The order view</returns>
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DetailsAdmin(int id)
        {
            var order = await _order.GetOrderById(id);
            order.BasketItems = await _item.GetBasketItems(order.BasketID);
            foreach (BasketItem item in order.BasketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
            }
            return View(order);
        }
    }
}
