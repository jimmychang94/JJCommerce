using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    public interface IInventory
    {
        Task<string> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts();
        Task<string> UpdateProduct(int id, Product product);
        Task<string> DeleteProduct(int id);
    }
}
