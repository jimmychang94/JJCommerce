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

        DbSet<Product> Products { get; set; }
    }
}
