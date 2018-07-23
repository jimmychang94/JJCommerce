using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JandJCommerce.Controllers
{
    [Authorize(Policy ="MemberOnly")]
    public class CheckoutController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IBasket _context;
        private IBasketItem _item;
        private IInventory _inventory;

        public CheckoutController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IBasket context, IBasketItem item, IInventory inventory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _item = item;
            _inventory = inventory;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basket = await _context.GetBasketById(user);
            var basketItems = await _item.GetBasketItems(basket.ID);
            CheckoutViewModel cvm = new CheckoutViewModel();
            cvm.TotalPrice = 0;
            foreach (BasketItem item in basketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
                cvm.TotalPrice += item.Product.Price * item.Quantity;
            }
            cvm.BasketItems = basketItems;

            return View(cvm);
        }

        public async Task<IActionResult> Summary()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basket = await _context.GetBasketById(user);
            var basketItems = await _item.GetBasketItems(basket.ID);
            CheckoutViewModel cvm = new CheckoutViewModel();
            cvm.TotalPrice = 0;
            foreach (BasketItem item in basketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
                cvm.TotalPrice += item.Product.Price * item.Quantity;
            }
            cvm.BasketItems = basketItems;
            await _context.DeleteBasket(basket.ID);
            return View(cvm);
        }
    }
}