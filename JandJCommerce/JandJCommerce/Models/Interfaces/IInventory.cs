﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Interfaces
{
    public interface IInventory
    {
        Task<IActionResult> CreateProduct(Product product);
        Task<IActionResult> GetProductById(int id);
        Task<IActionResult> GetProducts();
        Task<IActionResult> UpdateProduct(int id, Product product);
        Task<IActionResult> DeleteProduct(int id);
    }
}
