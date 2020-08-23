using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class timespanaddedinmenubranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceTime",
                schema: "Mealmate",
                table: "Menu",
                newName: "ServiceTimeTo");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ServiceTimeFrom",
                schema: "Mealmate",
                table: "Menu",
                type: "TIME(7)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceTimeFrom",
                schema: "Mealmate",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "ServiceTimeTo",
                schema: "Mealmate",
                table: "Menu",
                newName: "ServiceTime");
        }
    }
}
