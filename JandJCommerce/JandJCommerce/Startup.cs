﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JandJCommerce.Data;
using JandJCommerce.Models;
using JandJCommerce.Models.Handlers;
using JandJCommerce.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JandJCommerce
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CommerceDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("FurnitureConnection")));
            services.AddMvc();

            services.AddDbContext<ApplicationDbcontext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));
            services.AddMvc();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbcontext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["OAuth:Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["OAuth:Authentication:Google:ClientSecret"];
                })
            .AddMicrosoftAccount(microsoftOptions =>{
                microsoftOptions.ClientId = Configuration["OAuth:Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = Configuration["OAuth:Authentication:Microsoft:ClientSecret"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(ApplicationRoles.Admin));
                options.AddPolicy("MemberOnly", policy => policy.RequireRole(ApplicationRoles.Member));
                options.AddPolicy("Seattle", policy => policy.Requirements.Add(new LocationRequirement("Seattle")));
                options.AddPolicy("Cat", policy => policy.Requirements.Add(new LocationRequirement("Cat Stop")));
            });

            services.AddScoped<IInventory, DevIInventory>();
            services.AddScoped<IBasket, DevIBasket>();
            services.AddScoped<IBasketItem, DevIBasketItem>();
            services.AddScoped<IOrder, DevIOrder>();
            services.AddScoped<IAuthorizationHandler, LocationHandler>();
            services.AddScoped<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();


            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
