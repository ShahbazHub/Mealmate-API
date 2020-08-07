using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class AddBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillState",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillRequestId = table.Column<int>(type: "INT", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    BillStateId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_BillRequest",
                        column: x => x.BillRequestId,
                        principalSchema: "Request",
                        principalTable: "BillRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bill_BillState",
                        column: x => x.BillStateId,
                        principalSchema: "Lookup",
                        principalTable: "BillState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillDetail",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(type: "INT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    OrderId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetail_Bill",
                        column: x => x.BillId,
                        principalSchema: "Sale",
                        principalTable: "Bill",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_BillRequestId",
                schema: "Sale",
                table: "Bill",
                column: "BillRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bill_BillStateId",
                schema: "Sale",
                table: "Bill",
                column: "BillStateId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_BillId",
                schema: "Sale",
                table: "BillDetail",
                column: "BillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetail",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "Bill",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "BillState",
                schema: "Lookup");
        }
    }
}
