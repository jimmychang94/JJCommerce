using Microsoft.EntityFrameworkCore.Migrations;

namespace JandJCommerce.Migrations
{
    public partial class moreSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Category", "Image", "Sku" },
                values: new object[] { 7, "~/assets/desk1.jpg", "342asdf78" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Category", "Image", "Sku" },
                values: new object[] { 7, "~/assets/laptopstandTable2.jpg", "8234aa32" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Category", "Image", "Sku" },
                values: new object[] { 5, "~/assets/coffeetable.jpg", "345asdf324" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Category", "Image" },
                values: new object[] { 3, "~/assets/hutch.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Category", "Image" },
                values: new object[] { 1, "~/assets/couch2.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "Category", "Description", "Image" },
                values: new object[] { 6, "four drawer oak lock cabinet", "~/assets/Cabinets2.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7,
                column: "Image",
                value: "~/assets/rockingChair2.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 8,
                column: "Image",
                value: "~/assets/raceCarBed1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "Category", "Description", "Image" },
                values: new object[] { 4, "wood 6' by 6' 12\", 6 shelf, Walnut", "~/assets/shelves2.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "Category", "Description", "Image" },
                values: new object[] { 8, "Fully furnished ping pong table, comes with 2 paddles and 5 ping pong balls.", "~/assets/pingPong.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Category", "Image", "Sku" },
                values: new object[] { 8, "", "somerandomNumsAndLetters" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Category", "Image", "Sku" },
                values: new object[] { 8, "", "somerandomNumsAndLetters" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Category", "Image", "Sku" },
                values: new object[] { 6, "", "somerandomNumsAndLetters" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Category", "Image" },
                values: new object[] { 4, "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Category", "Image" },
                values: new object[] { 2, "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "Category", "Description", "Image" },
                values: new object[] { 7, "five drawer steel lock cabinet", "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7,
                column: "Image",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 8,
                column: "Image",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "Category", "Description", "Image" },
                values: new object[] { 5, "Black 6' by 6' 12\", 6 shelf, Walnut", "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "Category", "Description", "Image" },
                values: new object[] { 9, "fully furnished ping pong table, comes with 2 paddles and 5 ping pong balls.", "" });
        }
    }
}
