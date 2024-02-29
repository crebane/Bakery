using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryLabb.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartProducts");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "ShoppingCartProducts",
                newName: "ShoppingCartModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartProducts_ShoppingCartId",
                table: "ShoppingCartProducts",
                newName: "IX_ShoppingCartProducts_ShoppingCartModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartModelId",
                table: "ShoppingCartProducts",
                column: "ShoppingCartModelId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartModelId",
                table: "ShoppingCartProducts");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartModelId",
                table: "ShoppingCartProducts",
                newName: "ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartProducts_ShoppingCartModelId",
                table: "ShoppingCartProducts",
                newName: "IX_ShoppingCartProducts_ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartProducts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
