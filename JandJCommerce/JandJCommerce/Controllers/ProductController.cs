using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    public class ProductController : Controller
    {
        private IInventory _inventory;

        public ProductController(IInventory inventory)
        {
            _inventory = inventory;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}