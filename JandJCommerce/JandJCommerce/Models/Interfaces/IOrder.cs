using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    public interface IOrder
    {
        Task<string> CreateOrder(Order order);
        Task<Order> GetOrderByBasketId(int basketID);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrders();
        Task<List<Order>> GetOrdersByUserID(string userID);
        Task<string> DeleteOrder(int id);
        Task<string> UpdateOrder(int id, Order order);
    }
}
