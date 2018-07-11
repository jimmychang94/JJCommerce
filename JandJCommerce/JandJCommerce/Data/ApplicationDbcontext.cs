using JandJCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Data
{
    public class ApplicationDbcontext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options): base(options)
        {
            
        }
    }
}
