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

        public async Task<IViewComponentResult> InvokeAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var orders = await _context.GetOrdersByUserID(user.Id);
            if (orders == null)
            {
                return View();
            }
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
