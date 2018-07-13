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

        //private static async void AddUser(ApplicationDbcontext dbcontext, UserManager<ApplicationUser> userManager)
        //{
        //    if (dbcontext.Users.Any()) return;
        //    ApplicationUser admin = new ApplicationUser()
        //    {
        //        UserName = "Admin",
        //        Email = "furnitureAdmin@JJfurniture.com",
        //    };
        //    // I want to be able to add this to the user secrets; not sure how to right now
        //    string password = "@JJCommerce2";
        //    await userManager.AddPasswordAsync(admin, password);
        //    await userManager.CreateAsync(admin, password);
        //}

        //// I was trying to get this to work but couldn't think up how to finish it this way.
        //private static async void AddUserRoles(ApplicationDbcontext dbcontext)
        //{
        //    if (dbcontext.UserRoles.Any()) return;
        //    var admin = await dbcontext.Users.FirstOrDefaultAsync(s => s.UserName == "Admin");
        //    await 
        //}
    }
}
