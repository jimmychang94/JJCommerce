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

        public async Task<IViewComponentResult> InvokeAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserID == user.Id);
            if (basket == null)
            {
                return View(new Basket());
            }

            List<BasketItem> basketItems = await _context.BasketItems.Where(i => i.BasketID == basket.ID).ToListAsync();
            List<string> products = new List<string>();
            foreach (BasketItem item in basketItems)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.ID == item.ProductID);
                products.Add(product.Name);
            }
            return View(products);
        }
    }
}
