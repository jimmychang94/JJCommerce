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
                    Name = "",
                    Sku = "",
                    Description = "",
                    Image = "",
                    Price = 12.104M,
                    Category = "Desk",
                }
                );
        }

        public DbSet<Product> Products { get; set; }
    }
}
