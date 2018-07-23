using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<BasketItem> BasketItems { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
