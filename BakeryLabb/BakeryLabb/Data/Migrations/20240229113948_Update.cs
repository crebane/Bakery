using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryLabb.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "ShoppingCartProducts");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingCartProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShoppingCartProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCartProducts");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "ShoppingCartProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
