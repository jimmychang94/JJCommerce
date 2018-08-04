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
    /// This makes sure that only members are accessing this controller.
    /// </summary>
    [Authorize(Policy = "MemberOnly")]
    public class BasketItemController : Controller
    {
        private IBasketItem _context;
        private IBasket _basket;
        private UserManager<ApplicationUser> _userManager;

        public BasketItemController(IBasketItem context, IBasket basket, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _basket = basket;
            _userManager = userManager;
        }

        /// <summary>
        /// This method deletes the associated basketitem if it can be found and then redirects the user to the basket page
        /// </summary>
        /// <param name="id">The id of the basket item to remove</param>
        /// <returns>Redirects to the basket page</returns>
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var basket = await _basket.GetBasketById(user);
            var basketItem = await _context.GetBasketItemById(id);
            if (basket.ID != basketItem.BasketID)
            {
                return RedirectToAction("Index", "Home");
            }
            string result = await _context.DeleteBasketItem(id);

            if (result == "Product Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Details", "Basket");
        }

        /// <summary>
        /// This gets the basket item associated with the id and sends the user to an update page.
        /// </summary>
        /// <param name="id">The basket item to update</param>
        /// <returns>The update basket item page</returns>
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var basket = await _basket.GetBasketById(user);
            BasketItem basketItem = await _context.GetBasketItemById(id);
            if (basket.ID != basketItem.BasketID)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(basketItem);
        }

        /// <summary>
        /// This takes what was changed in the update form and then edits the corresponding basket item.
        /// It then returns the user to the basket page.
        /// </summary>
        /// <param name="id">The id of the basket to update</param>
        /// <param name="item">The updated basket</param>
        /// <returns>The basket page</returns>
        [HttpPost]
        public async Task<IActionResult> Update(int id, BasketItem item)
        {
            BasketItem basketItem = await _context.GetBasketItemById(item.ID);
            basketItem.Quantity = item.Quantity;
            string result = await _context.UpdateBasketItem(basketItem.ID, basketItem);

            if (result == "Basket Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Details", "Basket");
        }
    }
}
