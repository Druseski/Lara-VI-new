using Microsoft.EntityFrameworkCore.Migrations;

namespace Lara_VI.Data.Migrations
{
    public partial class AddEmailToInquiryHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "InquiryHeader",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "InquiryHeader");
        }
    }
}
