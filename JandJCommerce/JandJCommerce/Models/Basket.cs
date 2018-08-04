using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models
{
    /// <summary>
    /// This holds the properties that are in a basket
    /// </summary>
    public class Basket
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public bool IsProcessed { get; set; } = false;
    }
}
