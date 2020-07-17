using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class RemoveCuisineRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_CuisineType",
                schema: "Mealmate",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_CuisineTypeId",
                schema: "Mealmate",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "CuisineTypeId",
                schema: "Mealmate",
                table: "Restaurant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuisineTypeId",
                schema: "Mealmate",
                table: "Restaurant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_CuisineTypeId",
                schema: "Mealmate",
                table: "Restaurant",
                column: "CuisineTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_CuisineType",
                schema: "Mealmate",
                table: "Restaurant",
                column: "CuisineTypeId",
                principalSchema: "Lookup",
                principalTable: "CuisineType",
                principalColumn: "Id");
        }
    }
}
