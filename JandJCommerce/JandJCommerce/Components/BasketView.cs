using JandJCommerce.Data;
using JandJCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Components
{
    public class BasketView : ViewComponent
    {
        private CommerceDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public BasketView(CommerceDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// This runs the component which shows the user's current basket.
        /// This makes sure that the user is accessing a basket which has not been processed yet.
        /// </summary>
        /// <param name="userEmail">This is how we determine which user it is.</param>
        /// <returns>The component view</returns>
        public async Task<IViewComponentResult> InvokeAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            // The additional search for the IsProcessed == false is what makes sure that the basket has not been processed yet.
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserID == user.Id && b.IsProcessed == false);
            if (basket == null)
            {
                return View(new List<BasketItem>());
            }
            //This call and the calls in the foreach make sure that there are actual basket items with products to display.
            List<BasketItem> basketItems = await _context.BasketItems.Where(i => i.BasketID == basket.ID).ToListAsync();
            foreach (BasketItem item in basketItems)
            {
                item.Product = await _context.Products.FirstOrDefaultAsync(p => p.ID == item.ProductID);
            }
            return View(basketItems);
        }
    }
}
