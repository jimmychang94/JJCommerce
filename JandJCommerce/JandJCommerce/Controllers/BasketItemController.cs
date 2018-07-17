using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    public class BasketItemController : Controller
    {
        private IBasketItem _context;

        public BasketItemController(IBasketItem context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string result = await _context.DeleteBasketItem(id);

            if (result == "Product Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Details", "Basket");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            BasketItem basketItem = await _context.GetBasketItemById(id);
            return View(basketItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BasketItem basketItem)
        {
            string result = await _context.UpdateBasketItem(basketItem.ID, basketItem);

            if (result == "Basket Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Details", "Basket");
        }
    }
}
