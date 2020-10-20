using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyOrderCalc.EntityFramework.Migrations
{
    public partial class oresupplytype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SupplyTypes",
                columns: new[] { "Id", "CorpCreditPercent", "Guid", "Name", "PricePercentModifier" },
                values: new object[] { 5, 3.0, new Guid("00000000-0000-0000-0000-000000000000"), "Misc Ore", 95 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SupplyTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
