using System;
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
using Microsoft.Extensions.Configuration;

namespace JandJCommerce.Controllers
{
    [Authorize(Policy ="MemberOnly")]
    public class CheckoutController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IBasket _context;
        private IBasketItem _item;
        private IOrder _order;
        private IInventory _inventory;
        private IEmailSender _emailSender;
        private IConfiguration Configuration;


        public CheckoutController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IBasket context, IBasketItem item, IOrder order, IInventory inventory, IEmailSender emailSender, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _item = item;
            _order = order;
            _inventory = inventory;
            _emailSender = emailSender;
            Configuration = configuration;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basket = await _context.GetBasketById(user);
            var basketItems = await _item.GetBasketItems(basket.ID);
            var order = await _order.GetOrderByBasketId(basket.ID);
            if (order == null || order.IsProcessed == true)
            {
                order = new Order
                {
                    UserID = user.Id,
                    BasketID = basket.ID,
                    TotalPrice = 0
                };
                foreach (BasketItem item in basketItems)
                {
                    item.Product = await _inventory.GetProductById(item.ProductID);
                    order.TotalPrice += item.Product.Price * item.Quantity;
                }
                order.BasketItems = basketItems;
                await _order.CreateOrder(order);
            }
            else
            {
                foreach (BasketItem item in basketItems)
                {
                    item.Product = await _inventory.GetProductById(item.ProductID);
                }
                order.BasketItems = basketItems;
            }
            CheckoutViewModel cvm = new CheckoutViewModel()
            {
                Order = order,

            };

           
            return View(cvm);
        }



        public async Task<IActionResult> Summary(CheckoutViewModel cvm)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basket = await _context.GetBasketById(user);
            var order = await _order.GetOrderByBasketId(basket.ID);
            var basketItems = await _item.GetBasketItems(order.BasketID);

            ChargeCreditCard creditCharge = new ChargeCreditCard(Configuration);
            creditCharge.RunCard();
            ///credit card logic goes here somewhere---

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<h3>Thank you for shopping with J and J Furniture</h3>");
            stringBuilder.Append("<h4>Here is a receipt of your purchases</h4>");
            stringBuilder.Append("<h6>Please save this for your own records</h6>");
            stringBuilder.Append("<ol>");

            foreach (BasketItem item in basketItems)
            {
                item.Product = await _inventory.GetProductById(item.ProductID);
                decimal productTotal = item.Product.Price * item.Quantity;
                stringBuilder.Append("<li>");
                stringBuilder.Append("<ul>");
                stringBuilder.Append($"<li>Name: {item.Product.Name}</li>");
                stringBuilder.Append($"<li>Quantity: {item.Quantity}</li>");
                stringBuilder.Append($"<li>Price: {item.Product.Price}</li>");
                stringBuilder.Append($"<li>Total Price: {productTotal}</li>");
                stringBuilder.Append("</ul>");
                stringBuilder.Append("</li>");
            }

            order.BasketItems = basketItems;
            basket.IsProcessed = true;
            order.IsProcessed = true;
            order.OrderDate = DateTime.Today;
            var update = await _order.UpdateOrder(order.ID, order);

            if (update == "Order Not Found")
            {
                return RedirectToAction("Index", "Home");
            }

            stringBuilder.Append("</ol>");
            stringBuilder.Append($"<p>Grand Total: {order.TotalPrice}</p>");
            stringBuilder.Append("<h5>Thank you for shopping with us!</h5>");
            stringBuilder.Append("<h6>We would be honored to have you visit again!</h6>");
            string msg = stringBuilder.ToString();

            await _emailSender.SendEmailAsync(user.Email, "J and J Furniture Receipt", msg);
            
            await _context.UpdateBasket(basket.ID, basket);
            return View(order);
        }
    }
}