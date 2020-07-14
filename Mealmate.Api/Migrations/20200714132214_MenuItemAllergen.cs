using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class MenuItemAllergen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItemAllergen",
                schema: "Mealmate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    AllergenId = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemAllergen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemAllergen_Allergen_AllergenId",
                        column: x => x.AllergenId,
                        principalSchema: "Lookup",
                        principalTable: "Allergen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemAllergen_MenuItem",
                        column: x => x.MenuItemId,
                        principalSchema: "Mealmate",
                        principalTable: "MenuItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemAllergen_AllergenId",
                schema: "Mealmate",
                table: "MenuItemAllergen",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemAllergen_MenuItemId",
                schema: "Mealmate",
                table: "MenuItemAllergen",
                column: "MenuItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemAllergen",
                schema: "Mealmate");
        }
    }
}
