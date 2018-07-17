using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    //[Authorize(Policy ="MemberOnly")]
    public class BasketController : Controller
    {
        private IBasket _context;

        public BasketController(IBasket context)
        {
            _context = context;
        }

        //[Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Index()
        {
            List<Basket> baskets = await _context.GetBaskets();
            return View(baskets);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Basket basket = _context.GetBasketById(id).Result;
            return View(basket);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string result = await _context.DeleteBasket(id);

            if (result == "Basket Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Basket basket = await _context.GetBasketById(id);
            return View(basket);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Basket basket)
        {
            string result = await _context.UpdateBasket(basket.ID, basket);

            if (result == "Basket Not Found")
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}