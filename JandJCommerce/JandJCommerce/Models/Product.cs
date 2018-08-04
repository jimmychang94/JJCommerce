using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models
{
    /// <summary>
    /// This holds all of our properties for each product
    /// </summary>
    public class Product
    {
        public int ID { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
    }

    public enum Category
    {
        Chairs,//0
        Beds,//1
        Couches,//2
        Dressers,//3
        Shelves,//4
        Tables,//5
        Cabinets,//6
        Desks,//7
        Other//8
    }
}
