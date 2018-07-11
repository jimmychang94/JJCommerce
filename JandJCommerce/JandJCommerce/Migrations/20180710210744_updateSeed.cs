using Microsoft.EntityFrameworkCore.Migrations;

namespace JandJCommerce.Migrations
{
    public partial class updateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Category", "Description", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 1, 8, "Desk measures 62\" x 48\" long edges, with a depth of 32\" on the short side.Tabletop is white and hardware is brushed grey. Legs are 29\" tall ", "", "Galant Corner Desk", 12.104m, "somerandomNumsAndLetters" },
                    { 2, 8, "White 4 foot tall Desk", "", "LapTop Stand", 29.99m, "somerandomNumsAndLetters" },
                    { 3, 6, "Brown round table, oak", "", "Coffee Table", 45.00m, "somerandomNumsAndLetters" },
                    { 4, 4, "4 1/2 feet long, 18\" wide 78", "", "Wood Hutch", 23.00m, "24asf45b" },
                    { 5, 2, "Plush leather with tan accents", "", "Brown Leather Couch", 54.95m, "567hgf35s" },
                    { 6, 7, "five drawer steel lock cabinet", "", "File Cabinet", 15.00m, "001af293d" },
                    { 7, 0, "Maple rocking chair, pristine condition, dark polish", "", "Rocking Chair", 100.00m, "834lsefr" },
                    { 8, 1, "Red plastic duriable kid size bed shaped like a corvette", "", "Hot Rod Bed", 120.00m, "330n534h" },
                    { 9, 5, "Black 6' by 6' 12\", 6 shelf, Walnut", "", "Book Shelf", 75.00m, "009hbk43p" },
                    { 10, 9, "fully furnished ping pong table, comes with 2 paddles and 5 ping pong balls.", "", "Ping Pong Table", 65.00m, "2234kk5b" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 10);
        }
    }
}
