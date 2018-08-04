using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models
{
    /// <summary>
    /// This holds the properties that are in basket items.
    /// </summary>
    public class BasketItem
    {
        public int ID { get; set; }
        public int BasketID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
