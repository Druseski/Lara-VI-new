using Microsoft.EntityFrameworkCore.Migrations;

namespace Lara_VI.Data.Migrations
{
    public partial class AddedExistInCartInPProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExistInCart",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExistInCart",
                table: "Product");
        }
    }
}
