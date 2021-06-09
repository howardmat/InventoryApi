using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class TenantToMaterialInv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "MaterialInventoryTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInventoryTransaction_TenantId",
                table: "MaterialInventoryTransaction",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialInventoryTransaction_Tenant_TenantId",
                table: "MaterialInventoryTransaction",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialInventoryTransaction_Tenant_TenantId",
                table: "MaterialInventoryTransaction");

            migrationBuilder.DropIndex(
                name: "IX_MaterialInventoryTransaction_TenantId",
                table: "MaterialInventoryTransaction");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "MaterialInventoryTransaction");
        }
    }
}
