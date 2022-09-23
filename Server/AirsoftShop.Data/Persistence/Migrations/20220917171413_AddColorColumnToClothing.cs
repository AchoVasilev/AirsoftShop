using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftShop.Data.Persistence.Migrations
{
    public partial class AddColorColumnToClothing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Clothings",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Clothings");
        }
    }
}
