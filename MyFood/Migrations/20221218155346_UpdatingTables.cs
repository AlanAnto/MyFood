using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "OrderPlaced",
                table: "FoodOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FoodOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrders_UserId",
                table: "FoodOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrders_AspNetUsers_UserId",
                table: "FoodOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrders_AspNetUsers_UserId",
                table: "FoodOrders");

            migrationBuilder.DropIndex(
                name: "IX_FoodOrders_UserId",
                table: "FoodOrders");

            migrationBuilder.DropColumn(
                name: "OrderPlaced",
                table: "FoodOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FoodOrders");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
