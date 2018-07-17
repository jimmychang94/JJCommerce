using JandJCommerce.Data;
using JandJCommerce.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models
{
    public class DevIBasket : IBasket
    {
        public CommerceDbContext _context { get; }

        private readonly IConfiguration Configuration;

        public DevIBasket(CommerceDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<string> CreateBasket(ApplicationUser user)
        {
            Basket basket = new Basket()
            {
                UserID = user.Id
            };
            var result = await _context.Baskets.AddAsync(basket);
            if (result == null)
            {
                return "Basket not created";
            }
            await _context.SaveChangesAsync();
            return "Basket created";
        }

        public async Task<string> DeleteBasket(int id)
        {
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(b => b.ID == id);
            if (basket == null)
            {
                return "Basket not found";
            }
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();

            return "Basket Removed";
        }

        public async Task<Basket> GetBasketById(int id)
        {
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(b => b.ID == id);
            return basket;
        }

        public async Task<List<Basket>> GetBaskets()
        {
            List<Basket> baskets = await _context.Baskets.ToListAsync();
            return baskets;
        }

        public async Task<string> UpdateBasket(int id, Basket basket)
        {
            Basket result = await _context.Baskets.FirstOrDefaultAsync(b => b.ID == id);
            if (result == null)
            {
                return "Basket not found";
            }
            result.BasketItems = basket.BasketItems;

            _context.Baskets.Update(result);
            await _context.SaveChangesAsync();

            return "Basket updated";
        }
    }
}
