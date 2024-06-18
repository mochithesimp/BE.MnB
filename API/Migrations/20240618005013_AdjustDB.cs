using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_VoucherId",
                table: "Order",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Voucher_VoucherId",
                table: "Order",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "VoucherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Voucher_VoucherId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_VoucherId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Order");
        }
    }
}
