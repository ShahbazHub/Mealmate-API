using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class GenderAndIsDisabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "Request",
                table: "RestroomRequest",
                type: "INT",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                schema: "Request",
                table: "RestroomRequest",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Request",
                table: "RestroomRequest");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                schema: "Request",
                table: "RestroomRequest");
        }
    }
}
