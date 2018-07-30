using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.ViewModels
{
    public class AdminViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
    }
}
