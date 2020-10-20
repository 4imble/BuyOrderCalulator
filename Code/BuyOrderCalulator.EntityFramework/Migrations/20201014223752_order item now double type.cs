using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyOrderCalc.EntityFramework.Migrations
{
    public partial class orderitemnowdoubletype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FixedUnitPrice",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FixedUnitPrice",
                table: "OrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
