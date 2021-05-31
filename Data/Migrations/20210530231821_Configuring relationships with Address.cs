using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ConfiguringrelationshipswithAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Province_ProvinceIsoCode_ProvinceCountryIsoCode",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ProvinceIsoCode_ProvinceCountryIsoCode",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ProvinceCountryIsoCode",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "CountryIsoCode",
                table: "Address",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceIsoCode_CountryIsoCode",
                table: "Address",
                columns: new[] { "ProvinceIsoCode", "CountryIsoCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address",
                column: "CountryIsoCode",
                principalTable: "Country",
                principalColumn: "IsoCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Province_ProvinceIsoCode_CountryIsoCode",
                table: "Address",
                columns: new[] { "ProvinceIsoCode", "CountryIsoCode" },
                principalTable: "Province",
                principalColumns: new[] { "IsoCode", "CountryIsoCode" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Province_ProvinceIsoCode_CountryIsoCode",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ProvinceIsoCode_CountryIsoCode",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "CountryIsoCode",
                table: "Address",
                type: "nvarchar(2)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCountryIsoCode",
                table: "Address",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Address",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceIsoCode_ProvinceCountryIsoCode",
                table: "Address",
                columns: new[] { "ProvinceIsoCode", "ProvinceCountryIsoCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Country_CountryIsoCode",
                table: "Address",
                column: "CountryIsoCode",
                principalTable: "Country",
                principalColumn: "IsoCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Province_ProvinceIsoCode_ProvinceCountryIsoCode",
                table: "Address",
                columns: new[] { "ProvinceIsoCode", "ProvinceCountryIsoCode" },
                principalTable: "Province",
                principalColumns: new[] { "IsoCode", "CountryIsoCode" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
