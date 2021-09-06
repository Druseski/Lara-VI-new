using Microsoft.EntityFrameworkCore.Migrations;

namespace Lara_VI.Data.Migrations
{
    public partial class RemoveExistInCartInPProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExistInCart",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExistInCart",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
