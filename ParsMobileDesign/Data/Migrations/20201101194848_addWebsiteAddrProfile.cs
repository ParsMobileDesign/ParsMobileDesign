using Microsoft.EntityFrameworkCore.Migrations;

namespace ParsMobileDesign.Data.Migrations
{
    public partial class addWebsiteAddrProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebsiteAddr",
                table: "Portfolio",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebsiteAddr",
                table: "Portfolio");
        }
    }
}
