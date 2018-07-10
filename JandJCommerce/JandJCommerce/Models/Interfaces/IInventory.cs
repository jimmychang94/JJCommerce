using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    interface IInventory
    {
        Task<IActionResult> CreatePost(Product product);
        Task<IActionResult> GetPostById(int id);
        Task<IActionResult> GetPosts();
        Task<IActionResult> UpdatePost(int id, Product product);
        Task<IActionResult> DeletePost(int id);
    }
}
