using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class CuisineType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuisineTypeId",
                schema: "Mealmate",
                table: "Restaurant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CuisineType",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuisineType", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_CuisineType",
                schema: "Mealmate",
                table: "Restaurant");

            migrationBuilder.DropTable(
                name: "CuisineType",
                schema: "Lookup");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_CuisineTypeId",
                schema: "Mealmate",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "CuisineTypeId",
                schema: "Mealmate",
                table: "Restaurant");
        }
    }
}
