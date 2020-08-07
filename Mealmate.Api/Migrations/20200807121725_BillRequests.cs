using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class BillRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillRequestState",
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
                    table.PrimaryKey("PK_BillRequestState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillRequest",
                schema: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "INT", nullable: false),
                    TableId = table.Column<int>(type: "INT", nullable: false),
                    RequestTime = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    ResponseTime = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: true),
                    Remarks = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    BillRequestStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillRequest_BillRequestState",
                        column: x => x.BillRequestStateId,
                        principalSchema: "Lookup",
                        principalTable: "BillRequestState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillRequest_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillRequest_Table",
                        column: x => x.TableId,
                        principalSchema: "Mealmate",
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillRequest_BillRequestStateId",
                schema: "Request",
                table: "BillRequest",
                column: "BillRequestStateId");

            migrationBuilder.CreateIndex(
                name: "IX_BillRequest_CustomerId",
                schema: "Request",
                table: "BillRequest",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillRequest_TableId",
                schema: "Request",
                table: "BillRequest",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillRequest",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "BillRequestState",
                schema: "Lookup");
        }
    }
}
