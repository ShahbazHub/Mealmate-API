using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class AddUserOtp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOtp",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Otp = table.Column<string>(nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "TIME(7)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TIME(7)", nullable: false),
                    UserId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOtp_User",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOtp_UserId",
                schema: "Identity",
                table: "UserOtp",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOtp",
                schema: "Identity");
        }
    }
}
