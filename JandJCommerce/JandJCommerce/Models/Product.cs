using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models
{
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
        Chairs,//1
        Beds,//2
        Couches,//3
        Dressers,//4
        Shelves,//5
        Tables,//6
        Cabinets,//7
        Desks,//8
        Other//9
    }
}
