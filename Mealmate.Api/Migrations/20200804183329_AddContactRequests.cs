using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class AddContactRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactRequestState",
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
                    table.PrimaryKey("PK_ContactRequestState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactRequest",
                schema: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "INT", nullable: false),
                    TableId = table.Column<int>(type: "INT", nullable: false),
                    RequestTime = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    ResponseTime = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: true, defaultValueSql: "GETDATE()"),
                    Remarks = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    ContactRequestStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactRequest_ContactRequestState",
                        column: x => x.ContactRequestStateId,
                        principalSchema: "Lookup",
                        principalTable: "ContactRequestState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContactRequest_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContactRequest_Table",
                        column: x => x.TableId,
                        principalSchema: "Mealmate",
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_ContactRequestStateId",
                schema: "Request",
                table: "ContactRequest",
                column: "ContactRequestStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_CustomerId",
                schema: "Request",
                table: "ContactRequest",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_TableId",
                schema: "Request",
                table: "ContactRequest",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactRequest",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "ContactRequestState",
                schema: "Lookup");
        }
    }
}
