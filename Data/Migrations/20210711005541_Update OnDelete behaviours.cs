using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateOnDeletebehaviours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address");

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
                name: "FK_Product_Formula_FormulaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Address_PrimaryAddressId",
                table: "Tenant");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_UserProfile_OwnerUserId",
                table: "Tenant");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address",
                column: "CountryIsoCode",
                principalTable: "Country",
                principalColumn: "IsoCode",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Formula_Category_CategoryId",
                table: "Formula",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Category_CategoryId",
                table: "Material",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Material",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Formula_FormulaId",
                table: "Product",
                column: "FormulaId",
                principalTable: "Formula",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Product",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Address_PrimaryAddressId",
                table: "Tenant",
                column: "PrimaryAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_UserProfile_OwnerUserId",
                table: "Tenant",
                column: "OwnerUserId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address");

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
                name: "FK_Product_Formula_FormulaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Address_PrimaryAddressId",
                table: "Tenant");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_UserProfile_OwnerUserId",
                table: "Tenant");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address",
                column: "CountryIsoCode",
                principalTable: "Country",
                principalColumn: "IsoCode",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Product_Formula_FormulaId",
                table: "Product",
                column: "FormulaId",
                principalTable: "Formula",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_UserProfile_OwnerUserId",
                table: "Tenant",
                column: "OwnerUserId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
