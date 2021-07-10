using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Updatingtoallownullsinsomefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "ProductInventoryTransaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryTransaction_ProductId",
                table: "ProductInventoryTransaction",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryTransaction_Product_ProductId",
                table: "ProductInventoryTransaction",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryTransaction_Product_ProductId",
                table: "ProductInventoryTransaction");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventoryTransaction_ProductId",
                table: "ProductInventoryTransaction");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "ProductInventoryTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
