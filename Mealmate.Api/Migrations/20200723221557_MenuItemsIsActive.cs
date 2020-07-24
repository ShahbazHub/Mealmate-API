using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class MenuItemsIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Mealmate",
                table: "MenuItemDietary",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Mealmate",
                table: "MenuItemAllergen",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Mealmate",
                table: "MenuItemDietary");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Mealmate",
                table: "MenuItemAllergen");
        }
    }
}
