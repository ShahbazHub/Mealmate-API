using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class AllergenDietOptItemIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Lookup",
                table: "OptionItemDietary",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Lookup",
                table: "OptionItemAllergen",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Lookup",
                table: "OptionItemDietary");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Lookup",
                table: "OptionItemAllergen");
        }
    }
}
