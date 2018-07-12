using JandJCommerce.Models.Interfaces;
using JandJCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JandJCommerce.Controllers
{
    public class HomeController : Controller
    {
        private IInventory _inventory;

        public HomeController(IInventory inventory)
        {
            _inventory = inventory;
        }

        public IActionResult Index (IndexUserViewModel iuvm)
        {
            //if (iuvm.MyClaims != null)
            //{
            //    foreach(Claim claim in iuvm.MyClaims)
            //    {
            //        if (claim.Type == ClaimTypes.Name)
            //        {
            //            iuvm.LoggedIn = true;
            //            iuvm.UserName = claim.Value;
            //        }
            //    }
            //}
            return View(iuvm);
        }
    }
}
