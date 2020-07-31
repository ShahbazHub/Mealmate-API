using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class RefreshTokenChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserId1",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_UserId1",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Identity",
                table: "RefreshToken",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "Identity",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserId",
                schema: "Identity",
                table: "RefreshToken",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserId",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_UserId",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                schema: "Identity",
                table: "RefreshToken",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId1",
                schema: "Identity",
                table: "RefreshToken",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserId1",
                schema: "Identity",
                table: "RefreshToken",
                column: "UserId1",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
