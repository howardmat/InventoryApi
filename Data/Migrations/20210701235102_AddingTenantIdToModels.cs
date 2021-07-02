using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddingTenantIdToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Tenant_TenantId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ProductInventoryTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Formula",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Formula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryTransaction_TenantId",
                table: "ProductInventoryTransaction",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TenantId",
                table: "Product",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Formula_TenantId",
                table: "Formula",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Tenant_TenantId",
                table: "Category",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Formula_Tenant_TenantId",
                table: "Formula",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Tenant_TenantId",
                table: "Product",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryTransaction_Tenant_TenantId",
                table: "ProductInventoryTransaction",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Tenant_TenantId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Formula_Tenant_TenantId",
                table: "Formula");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Tenant_TenantId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryTransaction_Tenant_TenantId",
                table: "ProductInventoryTransaction");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventoryTransaction_TenantId",
                table: "ProductInventoryTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Product_TenantId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Formula_TenantId",
                table: "Formula");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProductInventoryTransaction");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Formula");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Formula",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Tenant_TenantId",
                table: "Category",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
