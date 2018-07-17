using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    public interface IBasketItem
    {
        Task<string> CreateBasketItem(BasketItem basketItem);
        Task<BasketItem> GetBasketItemById(int id);
        Task<List<BasketItem>> GetBasketItems(int BasketID);
        Task<string> UpdateBasketItem(int id, BasketItem basketItem);
        Task<string> DeleteBasketItem(int id);
    }
}
