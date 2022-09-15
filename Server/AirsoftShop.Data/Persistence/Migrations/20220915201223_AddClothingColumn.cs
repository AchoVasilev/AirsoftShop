using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftShop.Data.Persistence.Migrations
{
    public partial class AddClothingColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClothingId",
                table: "ItemsInWishList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClothingId",
                table: "ItemImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clothings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DealerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clothings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clothings_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clothings_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clothings_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsInWishList_ClothingId",
                table: "ItemsInWishList",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ClothingId",
                table: "ItemImages",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_CartId",
                table: "Clothings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_DealerId",
                table: "Clothings",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_IsDeleted",
                table: "Clothings",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_OrderId",
                table: "Clothings",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_SubCategoryId",
                table: "Clothings",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImages_Clothings_ClothingId",
                table: "ItemImages",
                column: "ClothingId",
                principalTable: "Clothings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsInWishList_Clothings_ClothingId",
                table: "ItemsInWishList",
                column: "ClothingId",
                principalTable: "Clothings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImages_Clothings_ClothingId",
                table: "ItemImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsInWishList_Clothings_ClothingId",
                table: "ItemsInWishList");

            migrationBuilder.DropTable(
                name: "Clothings");

            migrationBuilder.DropIndex(
                name: "IX_ItemsInWishList_ClothingId",
                table: "ItemsInWishList");

            migrationBuilder.DropIndex(
                name: "IX_ItemImages_ClothingId",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "ItemsInWishList");

            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "ItemImages");
        }
    }
}
