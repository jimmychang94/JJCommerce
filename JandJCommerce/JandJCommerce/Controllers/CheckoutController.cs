﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private IEmailSender _emailSender;

        public CheckoutController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IBasket context, IBasketItem item, IInventory inventory, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _item = item;
            _inventory = inventory;
            _emailSender = emailSender;
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

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<h3>Thank you for shopping with J and J Furniture</h3>");
            stringBuilder.Append("<h4>Here is a receipt of your purchases</h4>");
            stringBuilder.Append("<h6>Please save this for your own records</h6>");
            stringBuilder.Append("<ol>");

            foreach (BasketItem item in basketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
                decimal productTotal = item.Product.Price * item.Quantity;
                cvm.TotalPrice += productTotal;
                stringBuilder.Append("<li>");
                stringBuilder.Append("<ul>");
                stringBuilder.Append($"<li>Name: {item.Product.Name}</li>");
                stringBuilder.Append($"<li>Quantity: {item.Quantity}</li>");
                stringBuilder.Append($"<li>Price: {item.Product.Price}</li>");
                stringBuilder.Append($"<li>Total Price: {productTotal}</li>");
                stringBuilder.Append("</ul>");
                stringBuilder.Append("</li>");
            }

            cvm.BasketItems = basketItems;

            stringBuilder.Append("</ol>");
            stringBuilder.Append($"<p>Grand Total: {cvm.TotalPrice}</p>");
            stringBuilder.Append("<h5>Thank you for shopping with us!</h5>");
            stringBuilder.Append("<h6>We would be honored to have you visit again!</h6>");
            string msg = stringBuilder.ToString();

            await _emailSender.SendEmailAsync(user.Email, "J and J Furniture Receipt", msg);

            await _context.DeleteBasket(basket.ID);
            return View(cvm);
        }
    }
}