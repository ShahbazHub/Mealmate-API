using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class IsActiveToUserOtp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Identity",
                table: "UserOtp",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Identity",
                table: "UserOtp");
        }
    }
}
