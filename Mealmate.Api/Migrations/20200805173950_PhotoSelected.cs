using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class PhotoSelected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoSelected",
                schema: "Lookup",
                table: "Dietary",
                type: "VARBINARY(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoSelected",
                schema: "Lookup",
                table: "Allergen",
                type: "VARBINARY(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoSelected",
                schema: "Lookup",
                table: "Dietary");

            migrationBuilder.DropColumn(
                name: "PhotoSelected",
                schema: "Lookup",
                table: "Allergen");
        }
    }
}
