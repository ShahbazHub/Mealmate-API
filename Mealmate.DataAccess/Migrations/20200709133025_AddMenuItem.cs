using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.DataAccess.Migrations
{
    public partial class AddMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItem",
                schema: "Mealmate",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    MenuId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItem_Menu",
                        column: x => x.MenuId,
                        principalSchema: "Mealmate",
                        principalTable: "Menu",
                        principalColumn: "MenuId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuId",
                schema: "Mealmate",
                table: "MenuItem",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItem",
                schema: "Mealmate");
        }
    }
}
