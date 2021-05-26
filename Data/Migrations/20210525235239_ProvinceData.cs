using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace Data.Migrations
{
    public partial class ProvinceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = File.ReadAllText("../Data/Migrations/RawSql/20210524 2054 ProvinceSeed.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Province");
        }
    }
}
