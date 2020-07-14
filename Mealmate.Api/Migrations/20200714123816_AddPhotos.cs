using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class AddPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                schema: "Mealmate",
                table: "Restaurant",
                type: "VARBINARY(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                schema: "Lookup",
                table: "Dietary",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                schema: "Lookup",
                table: "Allergen",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                schema: "Mealmate",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "Photo",
                schema: "Lookup",
                table: "Dietary");

            migrationBuilder.DropColumn(
                name: "Photo",
                schema: "Lookup",
                table: "Allergen");
        }
    }
}
