using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class servicetimesaddedinbranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ServiceTimeFrom",
                schema: "Mealmate",
                table: "Branch",
                type: "DATETIMEOFFSET",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ServiceTimeTo",
                schema: "Mealmate",
                table: "Branch",
                type: "DATETIMEOFFSET",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceTimeFrom",
                schema: "Mealmate",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "ServiceTimeTo",
                schema: "Mealmate",
                table: "Branch");
        }
    }
}
