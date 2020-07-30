using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class AddOrderState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStateId",
                schema: "Sale",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderState",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStateId",
                schema: "Sale",
                table: "Order",
                column: "OrderStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderState",
                schema: "Sale",
                table: "Order",
                column: "OrderStateId",
                principalSchema: "Lookup",
                principalTable: "OrderState",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderState",
                schema: "Sale",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderState",
                schema: "Lookup");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderStateId",
                schema: "Sale",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderStateId",
                schema: "Sale",
                table: "Order");
        }
    }
}
