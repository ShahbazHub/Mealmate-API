using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class MakeResponseTimeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ResponseTime",
                schema: "Request",
                table: "RestroomRequest",
                type: "DATETIMEOFFSET",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIMEOFFSET",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ResponseTime",
                schema: "Request",
                table: "ContactRequest",
                type: "DATETIMEOFFSET",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIMEOFFSET",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ResponseTime",
                schema: "Request",
                table: "RestroomRequest",
                type: "DATETIMEOFFSET",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIMEOFFSET",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ResponseTime",
                schema: "Request",
                table: "ContactRequest",
                type: "DATETIMEOFFSET",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIMEOFFSET",
                oldNullable: true);
        }
    }
}
