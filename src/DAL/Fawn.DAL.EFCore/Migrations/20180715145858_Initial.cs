using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fawn.DAL.EFCore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    PickupDateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GoodsId = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsImages_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ItemPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsPrice_Goods_Id",
                        column: x => x.Id,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGoods",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    GoodsId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    ItemPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGoods", x => new { x.OrderId, x.GoodsId });
                    table.ForeignKey(
                        name: "FK_OrderGoods_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderGoods_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Tasty latte", "Latte" },
                    { 2, "Tasty milk for you!", "Milk" },
                    { 3, null, "Cappucino" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedDateTime", "CustomerId", "IsPaid", "PickupDateTime", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 7, 15, 14, 58, 58, 2, DateTimeKind.Utc), null, false, new DateTime(2018, 7, 15, 15, 18, 58, 2, DateTimeKind.Utc), (byte)0 },
                    { 2, new DateTime(2018, 7, 15, 14, 58, 58, 2, DateTimeKind.Utc), null, true, new DateTime(2018, 7, 15, 15, 8, 58, 2, DateTimeKind.Utc), (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "GoodsPrice",
                columns: new[] { "Id", "ItemPrice" },
                values: new object[,]
                {
                    { 1, 10.5m },
                    { 2, 4m },
                    { 3, 5m }
                });

            migrationBuilder.InsertData(
                table: "OrderGoods",
                columns: new[] { "OrderId", "GoodsId", "Amount", "ItemPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 5m },
                    { 1, 2, 1, 4.5m },
                    { 2, 3, 3, 7m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsImages_GoodsId",
                table: "GoodsImages",
                column: "GoodsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderGoods_GoodsId",
                table: "OrderGoods",
                column: "GoodsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsImages");

            migrationBuilder.DropTable(
                name: "GoodsPrice");

            migrationBuilder.DropTable(
                name: "OrderGoods");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
