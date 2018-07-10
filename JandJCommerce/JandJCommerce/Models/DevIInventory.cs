using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Data;
using Microsoft.Extensions.Configuration;

namespace JandJCommerce.Models
{
    public class DevIInventory : IInventory
    {
        private DbContext _context;
        private readonly IConfiguration Configuration;

        public DevIInventory(DbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
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
