using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyOrderCalc.EntityFramework.Migrations
{
    public partial class refiningskillsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefinementSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    Quality = table.Column<int>(nullable: false),
                    Efficiency = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefinementSkills", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RefinementSkills",
                columns: new[] { "Id", "Efficiency", "Guid", "Quality" },
                values: new object[,]
                {
                    { 1, 60.0, new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { 2, 60.0, new Guid("00000000-0000-0000-0000-000000000000"), 1 },
                    { 3, 60.0, new Guid("00000000-0000-0000-0000-000000000000"), 2 },
                    { 4, 60.0, new Guid("00000000-0000-0000-0000-000000000000"), 3 },
                    { 5, 52.5, new Guid("00000000-0000-0000-0000-000000000000"), 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefinementSkills");
        }
    }
}
