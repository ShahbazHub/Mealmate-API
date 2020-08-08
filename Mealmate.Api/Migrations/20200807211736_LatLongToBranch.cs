using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class LatLongToBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "Mealmate",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "Mealmate",
                table: "Branch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "Mealmate",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "Mealmate",
                table: "Branch");
        }
    }
}
