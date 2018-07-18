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
    public class DevIBasketItem : IBasketItem
    {
        public CommerceDbContext _context { get; }

        private readonly IConfiguration Configuration;

        public DevIBasketItem(CommerceDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<string> CreateBasketItem(BasketItem basketItem)
        {
            var result = await _context.BasketItems.AddAsync(basketItem);
            if (result == null)
            {
                return "Basket Item not created";
            }
            await _context.SaveChangesAsync();
            return "Basket Item created";
        }

        public async Task<string> DeleteBasketItem(int id)
        {
            BasketItem basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.ID == id);
            if (basketItem == null)
            {
                return "Basket Item not found";
            }
            _context.BasketItems.Remove(basketItem);
            await _context.SaveChangesAsync();

            return "Basket Item Removed";
        }

        public async Task<BasketItem> GetBasketItemById(int id)
        {
            BasketItem basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.ID == id);
            return basketItem;
        }

        public async Task<List<BasketItem>> GetBasketItems(int BasketID)
        {
            List<BasketItem> basketItems = await _context.BasketItems.Where(b => b.BasketID == BasketID).ToListAsync();
            return basketItems;
        }

        public async Task<string> UpdateBasketItem(int id, BasketItem basketItem)
        {
            BasketItem result = await _context.BasketItems.FirstOrDefaultAsync(b => b.ID == id);
            if (result == null)
            {
                return "Basket Item not found";
            }
            result.Product = basketItem.Product;
            result.Quantity = basketItem.Quantity;

            _context.BasketItems.Update(result);
            await _context.SaveChangesAsync();

            return "Basket Item updated";
        }
    }
}
