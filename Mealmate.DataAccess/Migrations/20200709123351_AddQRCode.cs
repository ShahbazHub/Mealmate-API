using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.DataAccess.Migrations
{
    public partial class AddQRCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QRCode",
                schema: "Mealmate",
                columns: table => new
                {
                    QRCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    TableId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCode", x => x.QRCodeId);
                    table.ForeignKey(
                        name: "FK_QRCode_Table",
                        column: x => x.TableId,
                        principalSchema: "Mealmate",
                        principalTable: "Table",
                        principalColumn: "TableId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QRCode_TableId",
                schema: "Mealmate",
                table: "QRCode",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QRCode",
                schema: "Mealmate");
        }
    }
}
