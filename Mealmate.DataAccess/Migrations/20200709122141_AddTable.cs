using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.DataAccess.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Table",
                schema: "Mealmate",
                columns: table => new
                {
                    TableId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    LocationId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.TableId);
                    table.ForeignKey(
                        name: "FK_Table_Location",
                        column: x => x.LocationId,
                        principalSchema: "Mealmate",
                        principalTable: "Location",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Table_LocationId",
                schema: "Mealmate",
                table: "Table",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Table",
                schema: "Mealmate");
        }
    }
}
