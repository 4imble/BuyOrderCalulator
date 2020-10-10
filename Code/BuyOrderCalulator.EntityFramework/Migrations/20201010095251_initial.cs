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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PricePercentModifier = table.Column<int>(nullable: false),
                    CorpCreditPercent = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    DiscordId = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    AccessToken = table.Column<string>(nullable: true),
                    TokenExpires = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    ApiId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MarketPrice = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ReorderLevel = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    SupplyTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_SupplyTypes_SupplyTypeId",
                        column: x => x.SupplyTypeId,
                        principalTable: "SupplyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    FixedUnitPrice = table.Column<int>(nullable: false),
                    FixedCorpCreditPercent = table.Column<double>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Guid", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000000"), "Unclassified" },
                    { 2, new Guid("00000000-0000-0000-0000-000000000000"), "Mineral" },
                    { 3, new Guid("00000000-0000-0000-0000-000000000000"), "Planetary" },
                    { 4, new Guid("00000000-0000-0000-0000-000000000000"), "Debt" },
                    { 5, new Guid("00000000-0000-0000-0000-000000000000"), "Wallet" }
                });

            migrationBuilder.InsertData(
                table: "SupplyTypes",
                columns: new[] { "Id", "CorpCreditPercent", "Guid", "Name", "PricePercentModifier" },
                values: new object[,]
                {
                    { 1, 2.0, new Guid("00000000-0000-0000-0000-000000000000"), "High", 80 },
                    { 2, 3.0, new Guid("00000000-0000-0000-0000-000000000000"), "Low", 90 },
                    { 3, 4.0, new Guid("00000000-0000-0000-0000-000000000000"), "Emergency", 105 },
                    { 4, 0.0, new Guid("00000000-0000-0000-0000-000000000000"), "Unwanted", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_SupplyTypeId",
                table: "Items",
                column: "SupplyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TypeId",
                table: "Items",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "SupplyTypes");

            migrationBuilder.DropTable(
                name: "ItemTypes");
        }
    }
}
