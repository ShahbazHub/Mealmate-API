using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.DataAccess.Migrations
{
    public partial class AddMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "Mealmate",
                columns: table => new
                {
                    MenuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    ServiceTime = table.Column<TimeSpan>(type: "TIME(7)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    BranchId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menu_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Mealmate",
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_BranchId",
                schema: "Mealmate",
                table: "Menu",
                column: "BranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu",
                schema: "Mealmate");
        }
    }
}
