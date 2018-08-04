using JandJCommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models
{
    /// <summary>
    /// This is how we initialize the user database with roles
    /// </summary>
    public class StartupDbInitializer
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole
            {
                Name = ApplicationRoles.Admin,
                NormalizedName = ApplicationRoles.Admin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole
            {
                Name = ApplicationRoles.Member,
                NormalizedName = ApplicationRoles.Member.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        };

        public static void SeedData (IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var dbContext =
                new ApplicationDbcontext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbcontext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
            }
        }

        private static void AddRoles(ApplicationDbcontext dbcontext)
        {
            if (dbcontext.Roles.Any()) return;
            foreach (IdentityRole role in Roles)
            {
                dbcontext.Roles.Add(role);
                dbcontext.SaveChanges();
            }
        }
    }
}
