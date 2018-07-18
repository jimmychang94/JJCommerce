using JandJCommerce.Data;
using JandJCommerce.Models;
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

        public BasketView(CommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserID == user.Id);
            if (basket == null)
            {
                basket = new Basket()
                {
                    UserID = user.Id,
                    BasketItems = new List<BasketItem>()
                };
                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
                return View(basket.BasketItems);
            }

            List<BasketItem> basketItems = await _context.BasketItems.Where(i => i.BasketID == basket.ID).ToListAsync();
            return View(basketItems);
        }
    }
}
