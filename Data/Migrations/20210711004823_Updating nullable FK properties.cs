using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdatingnullableFKproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formula_Category_CategoryId",
                table: "Formula");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Category_CategoryId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Address_PrimaryAddressId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "UnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "UnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "ProductInventoryTransaction");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "ProductInventoryTransaction");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "MaterialInventoryTransaction");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "MaterialInventoryTransaction");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "FormulaIngredient");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "FormulaIngredient");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Formula");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Formula");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "PrimaryAddressId",
                table: "Tenant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerUserId",
                table: "Tenant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasurementId",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasurementId",
                table: "Material",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Material",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Formula",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Formula_Category_CategoryId",
                table: "Formula",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Category_CategoryId",
                table: "Material",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Material",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Product",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Address_PrimaryAddressId",
                table: "Tenant",
                column: "PrimaryAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formula_Category_CategoryId",
                table: "Formula");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Category_CategoryId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Address_PrimaryAddressId",
                table: "Tenant");

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "UserProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "UserProfile",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "UnitOfMeasurement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "UnitOfMeasurement",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrimaryAddressId",
                table: "Tenant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerUserId",
                table: "Tenant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Tenant",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "Tenant",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "ProductInventoryTransaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "ProductInventoryTransaction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasurementId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "MaterialInventoryTransaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "MaterialInventoryTransaction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasurementId",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Material",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "Material",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "FormulaIngredient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "FormulaIngredient",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Formula",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Formula",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "Formula",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "Category",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Address",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedUtc",
                table: "Address",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Formula_Category_CategoryId",
                table: "Formula",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Category_CategoryId",
                table: "Material",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Material",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Product",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Address_PrimaryAddressId",
                table: "Tenant",
                column: "PrimaryAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
