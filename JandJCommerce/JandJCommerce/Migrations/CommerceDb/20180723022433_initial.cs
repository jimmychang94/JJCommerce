using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JandJCommerce.Migrations.CommerceDb
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sku = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BasketID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketID",
                        column: x => x.BasketID,
                        principalTable: "Baskets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Category", "Description", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 1, 7, "Desk measures 62\" x 48\" long edges, with a depth of 32\" on the short side.Tabletop is white and hardware is brushed grey. Legs are 29\" tall ", "\\assets\\desk1.jpg", "Galant Corner Desk", 12.10m, "342asdf78" },
                    { 2, 7, "White 4 foot tall Desk", "\\assets\\laptopstandTable2.jpg", "LapTop Stand", 29.99m, "8234aa32" },
                    { 3, 5, "Brown round table, oak", "\\assets\\coffeetable.jpg", "Coffee Table", 45.00m, "345asdf324" },
                    { 4, 3, "4 1/2 feet long, 18\" wide 78", "\\assets\\hutch.jpg", "Wood Hutch", 23.00m, "24asf45b" },
                    { 5, 1, "Plush leather with tan accents", "\\assets\\couch2.jpg", "Brown Leather Couch", 54.95m, "567hgf35s" },
                    { 6, 6, "four drawer oak lock cabinet", "\\assets\\Cabinets2.jpg", "File Cabinet", 15.00m, "001af293d" },
                    { 7, 0, "Maple rocking chair, pristine condition, dark polish", "\\assets\\rockingChair2.jpg", "Rocking Chair", 100.00m, "834lsefr" },
                    { 8, 1, "Red plastic duriable kid size bed shaped like a corvette", "\\assets\\raceCarBed1.jpg", "Hot Rod Bed", 120.00m, "330n534h" },
                    { 9, 4, "wood 6' by 6' 12\", 6 shelf, Walnut", "\\assets\\shelves2.jpg", "Book Shelf", 75.00m, "009hbk43p" },
                    { 10, 8, "Fully furnished ping pong table, comes with 2 paddles and 5 ping pong balls.", "\\assets\\pingPong.jpg", "Ping Pong Table", 65.00m, "2234kk5b" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketID",
                table: "BasketItems",
                column: "BasketID");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductID",
                table: "BasketItems",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
