using Microsoft.EntityFrameworkCore.Migrations;

namespace JandJCommerce.Migrations.CommerceDb
{
    public partial class basketProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "Baskets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "Baskets");
        }
    }
}
