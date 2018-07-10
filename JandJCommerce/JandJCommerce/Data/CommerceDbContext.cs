using JandJCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
                    Sku = "somerandomNumsAndLetters",
                    Description = "Desk measures 62\" x 48\" long edges, with a depth of 32\" on the short side.Tabletop is white and hardware is brushed grey. Legs are 29\" tall ",
                    Image = "",
                    Price = 12.104M,
                    Category = "Desks",
                },
                new
                {
                    ID = 2,
                    Name = "LapTop Stand",
                    Sku = "somerandomNumsAndLetters",
                    Description = "White 4 foot tall Desk",
                    Image = "",
                    Price = 29.99,
                    Category = "Desks"
                },
                 new
                 {
                     ID = 3,
                     Name = "Coffee Table",
                     Sku = "somerandomNumsAndLetters",
                     Description = "Brown round table, oak",
                     Image = "",
                     Price = 45.00M,
                     Category = "Tables"
                 },
                 new
                 {
                     ID = 4,
                     Name = "Wood Hutch",
                     Sku = "24asf45b",
                     Description = "4 1/2 feet long, 18\" wide 78",
                     Image = "",
                     Price = 23.00,
                     Category = "Dresser"
                 },
                new
                {
                    ID = 5,
                    Name = "Brown Leather Couch",
                    Sku = "567hgf35s",
                    Description = "Plush leather with tan accents",
                    Image = "",
                    Price = 54.95M,
                    Category = "Couches",
                },
                new
                {
                    ID = 6,
                    Name = "LapTop Stand",
                    Sku = "somerandomNumsAndLetters",
                    Description = "White 4 foot tall Desk",
                    Image = "",
                    Price = 29.99,
                    Category = "Desks"
                },
                 new
                 {
                     ID = 3,
                     Name = "Coffee Table",
                     Sku = "somerandomNumsAndLetters",
                     Description = "Brown round table, oak",
                     Image = "",
                     Price = 45.00M,
                     Category = "Tables"
                 },
                 new
                 {
                     ID = 4,
                     Name = "Wood Hutch",
                     Sku = "24asf45b",
                     Description = "4 1/2 feet long, 18\" wide 78",
                     Image = "",
                     Price = 23.00,
                     Category = (Category)3
                 },
                  new
                  {
                      ID = 3,
                      Name = "Coffee Table",
                      Sku = "somerandomNumsAndLetters",
                      Description = "Brown round table, oak",
                      Image = "",
                      Price = 45.00M,
                      Category = "Tables"
                  },
                 new
                 {
                     ID = 4,
                     Name = "Wood Hutch",
                     Sku = "24asf45b",
                     Description = "4 1/2 feet long, 18\" wide 78",
                     Image = "",
                     Price = 23.00,
                     Category = (Category)3
                 }
                );
        }

        public DbSet<Product> Products { get; set; }
    }
}
