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
    public class DevIOrder : IOrder
    {
        private CommerceDbContext _context;

        public IConfiguration Configuration { get; }

        public DevIOrder(CommerceDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<string> CreateOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            if (result == null)
            {
                return "Basket Item not created";
            }
            await _context.SaveChangesAsync();
            return "Basket Item created";
        }

        public async Task<string> DeleteOrder(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.ID == id);
            if (order == null)
            {
                return "Order not found";
            }
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(b => b.ID == order.BasketID);
            List<BasketItem> basketItems = await _context.BasketItems.Where(i => i.BasketID == order.BasketID).ToListAsync();
            foreach (BasketItem item in basketItems)
            {
                _context.BasketItems.Remove(item);
            }
            _context.Baskets.Remove(basket);
            _context.Orders.Remove(order);
            
            await _context.SaveChangesAsync();

            return "Order Removed";
        }

        public async Task<Order> GetOrderByBasketId(int basketID)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.BasketID == basketID);
            if (order == null)
            {
                return null;
            }
            order.BasketItems = await _context.BasketItems.Where(i => i.BasketID == order.BasketID).ToListAsync();
            return order;
        }
        public async Task<Order> GetOrderById(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.ID == id);
            if (order == null)
            {
                return null;
            }
            order.BasketItems = await _context.BasketItems.Where(i => i.BasketID == order.BasketID).ToListAsync();
            foreach(BasketItem item in order.BasketItems)
            {
                item.Product = await _context.Products.FirstOrDefaultAsync(p => p.ID == item.ProductID);
            }
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            List<Order> orders = await _context.Orders.OrderByDescending(o => o.ID).Take(20).ToListAsync();
            foreach(Order order in orders)
            {
                order.BasketItems = await _context.BasketItems.Where(i => i.BasketID == order.BasketID).ToListAsync();
            }
            return orders;
        }

        public async Task<string> UpdateOrder(int id, Order order)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.ID == id);
            if (result == null)
            {
                return "Order Not Found";
            }

            result.BasketItems = order.BasketItems;
            result.TotalPrice = order.TotalPrice;
            result.IsProcessed = order.IsProcessed;

            _context.Orders.Update(result);
            await _context.SaveChangesAsync();

            return "Update Successful";
        }

        public async Task<List<Order>> GetOrdersByUserID(string userID)
        {
            List<Order> orders = await _context.Orders.Where(o => o.UserID == userID).OrderByDescending(o => o.ID).Take(3).ToListAsync();
            if (orders == null)
            {
                return null;
            }
            foreach(Order order in orders)
            {
                order.BasketItems = await _context.BasketItems.Where(i => i.BasketID == order.BasketID).ToListAsync();
                foreach(BasketItem item in order.BasketItems)
                {
                    item.Product = await _context.Products.FirstOrDefaultAsync(p => p.ID == item.ProductID);
                }
            }
            return orders;
        }
    }
}
