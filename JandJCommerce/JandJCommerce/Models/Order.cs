﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models
{
    /// <summary>
    /// This holds all of our properties for each order
    /// </summary>
    public class Order
    {
        public int ID { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public string UserID { get; set; }
        public int BasketID { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
