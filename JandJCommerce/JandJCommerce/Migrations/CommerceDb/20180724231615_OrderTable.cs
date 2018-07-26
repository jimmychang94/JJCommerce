using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JandJCommerce.Migrations.CommerceDb
{
    public partial class OrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "BasketItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    BasketID = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    IsProcessed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_OrderID",
                table: "BasketItems",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Orders_OrderID",
                table: "BasketItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Orders_OrderID",
                table: "BasketItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_OrderID",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "BasketItems");
        }
    }
}
