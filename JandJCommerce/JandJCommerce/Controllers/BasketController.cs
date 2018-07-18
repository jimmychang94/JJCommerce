using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    //[Authorize(Policy ="MemberOnly")]
    public class BasketController : Controller
    {
        private IBasket _context;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public BasketController(IBasket context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Index()
        {
            List<Basket> baskets = await _context.GetBaskets();
            return View(baskets);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Basket basket = await _context.GetBasketById(user);
            if (basket == null)
            {
                basket = new Basket()
                {
                    UserID = user.Id,
                    BasketItems = new List<BasketItem>()
                };
            }
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
        public async Task<IActionResult> Update()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Basket basket = await _context.GetBasketById(user);
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