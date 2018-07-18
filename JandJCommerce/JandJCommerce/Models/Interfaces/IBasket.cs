using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    public interface IBasket
    {
        Task<string> CreateBasket(ApplicationUser user);
        Task<Basket> GetBasketById(ApplicationUser user);
        Task<List<Basket>> GetBaskets();
        Task<string> UpdateBasket(int id, Basket basket);
        Task<string> DeleteBasket(int id);
    }
}
