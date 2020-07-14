using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class MenuItemDietaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItemDietary",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    DietaryId = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemDietary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemDietary_Dietary_DietaryId",
                        column: x => x.DietaryId,
                        principalSchema: "Lookup",
                        principalTable: "Dietary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemDietary_MenuItem",
                        column: x => x.MenuItemId,
                        principalSchema: "Mealmate",
                        principalTable: "MenuItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemDietary_DietaryId",
                schema: "Mealmate",
                table: "MenuItemDietary",
                column: "DietaryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemDietary_MenuItemId",
                schema: "Mealmate",
                table: "MenuItemDietary",
                column: "MenuItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemDietary",
                schema: "Mealmate");
        }
    }
}
