﻿// <auto-generated />
using JandJCommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JandJCommerce.Migrations
{
    [DbContext(typeof(CommerceDbContext))]
    [Migration("20180713225216_moreSeedData")]
    partial class moreSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JandJCommerce.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("Sku");

                    b.HasKey("ID");

                    b.ToTable("Products");

                    b.HasData(
                        new { ID = 1, Category = 7, Description = "Desk measures 62\" x 48\" long edges, with a depth of 32\" on the short side.Tabletop is white and hardware is brushed grey. Legs are 29\" tall ", Image = "~/assets/desk1.jpg", Name = "Galant Corner Desk", Price = 12.104m, Sku = "342asdf78" },
                        new { ID = 2, Category = 7, Description = "White 4 foot tall Desk", Image = "~/assets/laptopstandTable2.jpg", Name = "LapTop Stand", Price = 29.99m, Sku = "8234aa32" },
                        new { ID = 3, Category = 5, Description = "Brown round table, oak", Image = "~/assets/coffeetable.jpg", Name = "Coffee Table", Price = 45.00m, Sku = "345asdf324" },
                        new { ID = 4, Category = 3, Description = "4 1/2 feet long, 18\" wide 78", Image = "~/assets/hutch.jpg", Name = "Wood Hutch", Price = 23.00m, Sku = "24asf45b" },
                        new { ID = 5, Category = 1, Description = "Plush leather with tan accents", Image = "~/assets/couch2.jpg", Name = "Brown Leather Couch", Price = 54.95m, Sku = "567hgf35s" },
                        new { ID = 6, Category = 6, Description = "four drawer oak lock cabinet", Image = "~/assets/Cabinets2.jpg", Name = "File Cabinet", Price = 15.00m, Sku = "001af293d" },
                        new { ID = 7, Category = 0, Description = "Maple rocking chair, pristine condition, dark polish", Image = "~/assets/rockingChair2.jpg", Name = "Rocking Chair", Price = 100.00m, Sku = "834lsefr" },
                        new { ID = 8, Category = 1, Description = "Red plastic duriable kid size bed shaped like a corvette", Image = "~/assets/raceCarBed1.jpg", Name = "Hot Rod Bed", Price = 120.00m, Sku = "330n534h" },
                        new { ID = 9, Category = 4, Description = "wood 6' by 6' 12\", 6 shelf, Walnut", Image = "~/assets/shelves2.jpg", Name = "Book Shelf", Price = 75.00m, Sku = "009hbk43p" },
                        new { ID = 10, Category = 8, Description = "Fully furnished ping pong table, comes with 2 paddles and 5 ping pong balls.", Image = "~/assets/pingPong.jpg", Name = "Ping Pong Table", Price = 65.00m, Sku = "2234kk5b" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
