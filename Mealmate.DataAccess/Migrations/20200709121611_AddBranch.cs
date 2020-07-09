using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.DataAccess.Migrations
{
    public partial class AddBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "Mealmate",
                columns: table => new
                {
                    BranchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR(1000)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    RestaurantId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_Restaurant",
                        column: x => x.RestaurantId,
                        principalSchema: "Mealmate",
                        principalTable: "Restaurant",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_RestaurantId",
                schema: "Mealmate",
                table: "Branch",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branch",
                schema: "Mealmate");
        }
    }
}
