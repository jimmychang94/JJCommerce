using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Data;

namespace JandJCommerce.Models
{
    public class DevIInventory : IInventory
    {
        private DbContext _context;

        public DevIInventory(DbContext context)
        {
            _context = context;
        }

        public Task<IActionResult> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
