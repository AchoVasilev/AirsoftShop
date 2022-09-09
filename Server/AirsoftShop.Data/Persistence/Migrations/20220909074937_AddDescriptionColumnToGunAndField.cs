using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftShop.Data.Persistence.Migrations
{
    public partial class AddDescriptionColumnToGunAndField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Guns",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Fields",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_IsDeleted",
                table: "WishLists",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_IsDeleted",
                table: "SubCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IsDeleted",
                table: "Orders",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsInWishList_IsDeleted",
                table: "ItemsInWishList",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_IsDeleted",
                table: "ItemImages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Images_IsDeleted",
                table: "Images",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Guns_IsDeleted",
                table: "Guns",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_IsDeleted",
                table: "Fields",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_IsDeleted",
                table: "Dealers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_IsDeleted",
                table: "Couriers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IsDeleted",
                table: "Clients",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IsDeleted",
                table: "Cities",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_IsDeleted",
                table: "CategoryImages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsDeleted",
                table: "Categories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_IsDeleted",
                table: "Carts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_IsDeleted",
                table: "Addresses",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishLists_IsDeleted",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_IsDeleted",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IsDeleted",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_ItemsInWishList_IsDeleted",
                table: "ItemsInWishList");

            migrationBuilder.DropIndex(
                name: "IX_ItemImages_IsDeleted",
                table: "ItemImages");

            migrationBuilder.DropIndex(
                name: "IX_Images_IsDeleted",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Guns_IsDeleted",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Fields_IsDeleted",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_IsDeleted",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Couriers_IsDeleted",
                table: "Couriers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_IsDeleted",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IsDeleted",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_CategoryImages_IsDeleted",
                table: "CategoryImages");

            migrationBuilder.DropIndex(
                name: "IX_Categories_IsDeleted",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Carts_IsDeleted",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_IsDeleted",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Guns");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Fields");
        }
    }
}
