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
        private CommerceDbContext _context { get; }
        private readonly IConfiguration Configuration;

        public DevIInventory(CommerceDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<string> CreateProduct(Product product)
        {
            var result = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            if (result != null)
            {
                return "Product Created";
            }
            return "Product Not Created";
        }

        public async Task<string> DeleteProduct(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.ID == id);
            if (product == null)
            {
                return "Product Not Found";
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return "Product Removed";
        }

        public Task<Product> GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefaultAsync(p => p.ID == id);
            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<string> UpdateProduct(int id, Product product)
        {
            Product result = await _context.Products.FirstOrDefaultAsync(p => p.ID == id);

            if (result == null)
            {
                return "Product Not Found";
            }

            result.Category = product.Category;
            result.Description = product.Description;
            result.Image = product.Image;
            result.Name = product.Name;
            result.Price = product.Price;
            result.Sku = product.Sku;

            _context.Products.Update(result);
            await _context.SaveChangesAsync();

            return "Product Updated";
        }
    }
}
