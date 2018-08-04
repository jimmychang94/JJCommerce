using JandJCommerce.Data;
using JandJCommerce.Models;
using JandJCommerce.Models.Interfaces;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Components
{
    public class OrderView : ViewComponent
    {
        private IOrder _context;
        private UserManager<ApplicationUser> _userManager;

        public OrderView(IOrder context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// This finds the first 3 orders of the user and returns it in a view component
        /// </summary>
        /// <param name="userEmail">This is how we determine which user we are accessing</param>
        /// <returns>The component view</returns>
        public async Task<IViewComponentResult> InvokeAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            // We are accessing the IOrder interface instead of the database directly so we can use the built in methods of the interface.
            var orders = await _context.GetOrdersByUserID(user.Id);
            if (orders == null)
            {
                return View();
            }
            // We send the orders through an order view model since there is more information that we want to grab than what is just in the database.
            List<OrderViewModel> ovms = new List<OrderViewModel>();
            foreach(Order order in orders)
            {
                ovms.Add(new OrderViewModel()
                {
                    Order = order,
                    UserName = $"{user.FirstName} {user.LastName}"
                });
            }
            return View(ovms);
        }
    }
}
