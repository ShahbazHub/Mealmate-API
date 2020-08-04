using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class RemoveQrCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Mealmate",
                table: "QRCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Code",
                schema: "Mealmate",
                table: "QRCode",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
