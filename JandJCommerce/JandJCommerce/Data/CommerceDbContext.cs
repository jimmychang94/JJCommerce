using JandJCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Data
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions<CommerceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<Product>().HasData(
                new
                {
                    ID = 1,
                    Name = "Galant Corner Desk",
                    Sku = "342asdf78",
                    Description = "Desk measures 62\" x 48\" long edges, with a depth of 32\" on the short side.Tabletop is white and hardware is brushed grey. Legs are 29\" tall ",
                    Image = @"\assets\desk1.jpg",
                    Price = 12.10M,
                    Category = (Category)7
                },
                new
                {
                    ID = 2,
                    Name = "LapTop Stand",
                    Sku = "8234aa32",
                    Description = "White 4 foot tall Desk",
                    Image = @"\assets\laptopstandTable2.jpg",
                    Price = 29.99M,
                    Category = (Category)7
                },
                 new
                 {
                     ID = 3,
                     Name = "Coffee Table",
                     Sku = "345asdf324",
                     Description = "Brown round table, oak",
                     Image = @"\assets\coffeetable.jpg",
                     Price = 45.00M,
                     Category = (Category)5
                 },
                 new
                 {
                     ID = 4,
                     Name = "Wood Hutch",
                     Sku = "24asf45b",
                     Description = "4 1/2 feet long, 18\" wide 78",
                     Image = @"\assets\hutch.jpg",
                     Price = 23.00M,
                     Category = (Category)3
                 },
                new
                {
                    ID = 5,
                    Name = "Brown Leather Couch",
                    Sku = "567hgf35s",
                    Description = "Plush leather with tan accents",
                    Image = @"\assets\couch2.jpg",
                    Price = 54.95M,
                    Category = (Category)1
                },
                new
                {
                    ID = 6,
                    Name = "File Cabinet",
                    Sku = "001af293d",
                    Description = "four drawer oak lock cabinet",
                    Image = @"\assets\Cabinets2.jpg",
                    Price = 15.00M,
                    Category = (Category)6
                },
                 new
                 {
                     ID = 7,
                     Name = "Rocking Chair",
                     Sku = "834lsefr",
                     Description = "Maple rocking chair, pristine condition, dark polish",
                     Image = @"\assets\rockingChair2.jpg",
                     Price = 100.00M,
                     Category = (Category)0
                 },
                 new
                 {
                     ID = 8,
                     Name = "Hot Rod Bed",
                     Sku = "330n534h",
                     Description = "Red plastic duriable kid size bed shaped like a corvette",
                     Image = @"\assets\raceCarBed1.jpg",
                     Price = 120.00M,
                     Category = (Category)1
                 },
                  new
                  {
                      ID = 9,
                      Name = "Book Shelf",
                      Sku = "009hbk43p",
                      Description = "wood 6' by 6' 12\", 6 shelf, Walnut",
                      Image = @"\assets\shelves2.jpg",
                      Price = 75.00M,
                      Category = (Category)4
                  },
                 new
                 {
                     ID = 10,
                     Name = "Ping Pong Table",
                     Sku = "2234kk5b",
                     Description = "Fully furnished ping pong table, comes with 2 paddles and 5 ping pong balls.",
                     Image = @"\assets\pingPong.jpg",
                     Price = 65.00M,
                     Category = (Category)8
                 }
                );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
       
    }
}
