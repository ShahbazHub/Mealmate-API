using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class OrderInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sale");

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "INT", nullable: false),
                    TableId = table.Column<int>(type: "INT", nullable: false),
                    OrderNumber = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Table",
                        column: x => x.TableId,
                        principalSchema: "Mealmate",
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<int>(type: "INT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    OrderId = table.Column<int>(type: "INT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_MenuItem",
                        column: x => x.MenuItemId,
                        principalSchema: "Mealmate",
                        principalTable: "MenuItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItem_Order",
                        column: x => x.OrderId,
                        principalSchema: "Sale",
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItemDetail",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<int>(type: "INT", nullable: false),
                    MenuItemOptionId = table.Column<int>(type: "INT", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemDetail_MenuItemOption",
                        column: x => x.MenuItemOptionId,
                        principalSchema: "Mealmate",
                        principalTable: "MenuItemOption",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItemDetail_OrderItem",
                        column: x => x.OrderItemId,
                        principalSchema: "Sale",
                        principalTable: "OrderItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "Sale",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TableId",
                schema: "Sale",
                table: "Order",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_MenuItemId",
                schema: "Sale",
                table: "OrderItem",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Sale",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDetail_MenuItemOptionId",
                schema: "Sale",
                table: "OrderItemDetail",
                column: "MenuItemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDetail_OrderItemId",
                schema: "Sale",
                table: "OrderItemDetail",
                column: "OrderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemDetail",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Sale");
        }
    }
}
