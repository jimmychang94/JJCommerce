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

        public Task<IActionResult> CreatePost(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetPostById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetPosts()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdatePost(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
