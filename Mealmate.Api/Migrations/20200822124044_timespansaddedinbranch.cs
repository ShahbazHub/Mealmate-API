using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class timespansaddedinbranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ServiceTimeTo",
                schema: "Mealmate",
                table: "Branch",
                type: "TIME",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIMEOFFSET");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ServiceTimeFrom",
                schema: "Mealmate",
                table: "Branch",
                type: "TIME",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIMEOFFSET");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ServiceTimeTo",
                schema: "Mealmate",
                table: "Branch",
                type: "DATETIMEOFFSET",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TIME");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ServiceTimeFrom",
                schema: "Mealmate",
                table: "Branch",
                type: "DATETIMEOFFSET",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TIME");
        }
    }
}
