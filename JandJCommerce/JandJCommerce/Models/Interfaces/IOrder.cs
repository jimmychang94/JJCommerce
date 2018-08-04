using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    /// <summary>
    /// This sets up the methods that interact with the orders
    /// </summary>
    public interface IOrder
    {
        Task<string> CreateOrder(Order order);
        Task<Order> GetOrderByBasketId(int basketID);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrders();
        Task<List<Order>> GetOrdersByUserID(string userID);
        Task<string> UpdateOrder(int id, Order order);
    }
}
