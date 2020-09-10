using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyOrderCalc.EntityFramework.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ReorderLevel = table.Column<int>(nullable: false),
                    ReorderCreditValue = table.Column<int>(nullable: false),
                    TakingOrders = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Guid", "Name" },
                values: new object[] { 1, new Guid("00000000-0000-0000-0000-000000000000"), "Mineral" });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Guid", "Name" },
                values: new object[] { 2, new Guid("00000000-0000-0000-0000-000000000000"), "Planetary" });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Guid", "Name" },
                values: new object[] { 3, new Guid("00000000-0000-0000-0000-000000000000"), "Debt" });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Guid", "Name" },
                values: new object[] { 4, new Guid("00000000-0000-0000-0000-000000000000"), "Wallet" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Guid", "Name", "Quantity", "ReorderCreditValue", "ReorderLevel", "TakingOrders", "TypeId", "UnitPrice" },
                values: new object[] { 1, new Guid("00000000-0000-0000-0000-000000000000"), "Tritanium", 17450000, 1, 18742950, true, 1, 2 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Guid", "Name", "Quantity", "ReorderCreditValue", "ReorderLevel", "TakingOrders", "TypeId", "UnitPrice" },
                values: new object[] { 2, new Guid("00000000-0000-0000-0000-000000000000"), "Pyerite", 6950000, 1, 5312610, true, 1, 18 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Guid", "Name", "Quantity", "ReorderCreditValue", "ReorderLevel", "TakingOrders", "TypeId", "UnitPrice" },
                values: new object[] { 3, new Guid("00000000-0000-0000-0000-000000000000"), "Mexallon", 2290000, 1, 1665210, true, 1, 32 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Guid", "Name", "Quantity", "ReorderCreditValue", "ReorderLevel", "TakingOrders", "TypeId", "UnitPrice" },
                values: new object[] { 4, new Guid("00000000-0000-0000-0000-000000000000"), "Isogen", 170000, 1, 284720, true, 1, 110 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Guid", "Name", "Quantity", "ReorderCreditValue", "ReorderLevel", "TakingOrders", "TypeId", "UnitPrice" },
                values: new object[] { 5, new Guid("00000000-0000-0000-0000-000000000000"), "Nocxium", 130000, 1, 67810, true, 1, 1000 });

            migrationBuilder.CreateIndex(
                name: "IX_Items_TypeId",
                table: "Items",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemTypes");
        }
    }
}
