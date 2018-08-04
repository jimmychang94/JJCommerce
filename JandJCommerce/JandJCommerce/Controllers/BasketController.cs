using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    // This policy makes sure that anyone who accesses this controller is a member
    [Authorize(Policy ="MemberOnly")]
    public class BasketController : Controller
    {
        private IBasket _context;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IBasketItem _item;
        private IInventory _inventory;

        public BasketController(IBasket context, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IBasketItem item, IInventory inventory)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _item = item;
            _inventory = inventory;
        }
        
        /// <summary>
        /// This gets the unprocessed basket associated with the specific user.
        /// </summary>
        /// <returns>The view of the details page</returns>
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Basket basket = await _context.GetBasketById(user);

            // This logic makes sure that if the user accesses this page without a basket or with only processed baskets
            // That a basket is created for them and they are assigned that basket.
            if (basket == null || basket.IsProcessed == true)
            {
                await _context.CreateBasket(user);
                basket = await _context.GetBasketById(user);
            }
            basket.BasketItems = await _item.GetBasketItems(basket.ID);

            foreach (BasketItem item in basket.BasketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
            }
            return View(basket);
        }

        /// <summary>
        /// This method takes in an id and adds the product associated with that id as a basket item.
        /// It then adds that basket item to the basket.
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <returns>The shop page</returns>
        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            // This gets the product associated with the id
            Product product = await _inventory.GetProductById(id);
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            // The above checks to see if the user is logged in, and if not then they are redirected to the login page.
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }

            Basket basket = await _context.GetBasketById(user);
            // If the user doesn't have a basket that is unprocessed then they get a basket created for them and then retrieved.
            if (basket == null || basket.IsProcessed == true)
            {
                await _context.CreateBasket(user);
                basket = await _context.GetBasketById(user);
            }

            // This creates the new basket item and relates it to the user's basket.
            BasketItem basketItem = new BasketItem()
            {
                Product = product,
                ProductID = product.ID,
                BasketID = basket.ID,
                Quantity = 1
            };
            await _item.CreateBasketItem(basketItem);

            return RedirectToAction("Index", "Shop");
        }
    }
}