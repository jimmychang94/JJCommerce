using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    /// <summary>
    /// This creates the methods that interact with the baskets
    /// </summary>
    public interface IBasket
    {
        Task<Basket> CreateBasket(ApplicationUser user);
        Task<Basket> GetBasketById(ApplicationUser user);
        Task<string> UpdateBasket(int id, Basket basket);
    }
}
