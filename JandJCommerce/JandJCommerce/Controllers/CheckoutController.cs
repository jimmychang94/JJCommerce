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
    /// <summary>
    /// This makes sure that the people checking out are logged in.
    /// </summary>
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
            IBasket context, IBasketItem item, IOrder order, IInventory inventory, 
            IEmailSender emailSender, IConfiguration configuration)
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

        /// <summary>
        /// This would be the first checkout page with the summary of what is in the basket
        /// It is also where you fill out the billing address and all that other information.
        /// If this was a real ecommerce site, we would be asking for your credit card information here as well.
        /// We would NOT be storing it no matter what but we would use it in our next method to process your order.
        /// </summary>
        /// <returns>The checkout page</returns>
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basket = await _context.GetBasketById(user);
            var basketItems = await _item.GetBasketItems(basket.ID);
            var order = await _order.GetOrderByBasketId(basket.ID);
            // This makes sure that we have a new order (that it isn't one that has been purchased already)
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
            // This else is for if there is already an order waiting but it hasn't been completed yet.
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

        /// <summary>
        /// This is where the user goes when the finish their order (filled out the billing information etc)
        /// We take in the information that they gave us and then process the order by sending information to Auth.Net
        /// Then we send them an e-mail receipt of what they ordered.
        /// We return them to a view of their order receipt.
        /// </summary>
        /// <param name="cvm">The checkout view model which we use to store sensitive information temporarily</param>
        /// <returns>The receipt page</returns>
        public async Task<IActionResult> Summary(CheckoutViewModel cvm)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basket = await _context.GetBasketById(user);
            var order = await _order.GetOrderByBasketId(basket.ID);
            var basketItems = await _item.GetBasketItems(order.BasketID);

            // This string builder is building out the message for the email.
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<h3>Thank you for shopping with J and J Furniture</h3>");
            stringBuilder.Append("<h4>Here is a receipt of your purchases</h4>");
            stringBuilder.Append("<h6>Please save this for your own records</h6>");
            stringBuilder.Append("<ol>");

            // This foreach loop is what fills out all the items in the order onto the receipt.
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
            cvm.Order = order;
            // This updates the order. The main things to be changed is that it has been processed and that the order date is today.
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

            // This is where your credit card gets charged.
            ChargeCreditCard creditCharge = new ChargeCreditCard(Configuration);
            creditCharge.RunCard(cvm);

            // This sends the user their receipt through email.
            await _emailSender.SendEmailAsync(user.Email, "J and J Furniture Receipt", msg);

            // This updates the basket so that it is processed and shouldn't be reused.
            await _context.UpdateBasket(basket.ID, basket);
            return View(order);
        }
    }
}