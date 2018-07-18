using Microsoft.EntityFrameworkCore.Migrations;

namespace JandJCommerce.Migrations
{
    public partial class changeBasketItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Products_ProductID",
                table: "BasketItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "BasketItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Products_ProductID",
                table: "BasketItems",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Products_ProductID",
                table: "BasketItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "BasketItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Products_ProductID",
                table: "BasketItems",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
