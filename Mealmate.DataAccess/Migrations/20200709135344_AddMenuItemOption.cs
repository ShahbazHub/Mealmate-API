using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.DataAccess.Migrations
{
    public partial class AddMenuItemOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.CreateTable(
                name: "OptionItem",
                schema: "Lookup",
                columns: table => new
                {
                    OptionItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionItem", x => x.OptionItemId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemOption",
                schema: "Mealmate",
                columns: table => new
                {
                    MenuItemOptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(10, 2)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    MenuItemId = table.Column<int>(type: "INT", nullable: false),
                    OptionItemId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemOption", x => x.MenuItemOptionId);
                    table.ForeignKey(
                        name: "FK_MenuItemOption_MenuItem",
                        column: x => x.MenuItemId,
                        principalSchema: "Mealmate",
                        principalTable: "MenuItem",
                        principalColumn: "MenuItemId");
                    table.ForeignKey(
                        name: "FK_MenuItemOption_OptionItem",
                        column: x => x.OptionItemId,
                        principalSchema: "Lookup",
                        principalTable: "OptionItem",
                        principalColumn: "OptionItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOption_MenuItemId",
                schema: "Mealmate",
                table: "MenuItemOption",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOption_OptionItemId",
                schema: "Mealmate",
                table: "MenuItemOption",
                column: "OptionItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemOption",
                schema: "Mealmate");

            migrationBuilder.DropTable(
                name: "OptionItem",
                schema: "Lookup");
        }
    }
}
