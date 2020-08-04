using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class RestroomRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Request");

            migrationBuilder.CreateTable(
                name: "RestroomRequestState",
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
                    table.PrimaryKey("PK_RestroomRequestState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestroomRequest",
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
                    RestroomRequestStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestroomRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestroomRequest_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RestroomRequest_RestroomRequestState",
                        column: x => x.RestroomRequestStateId,
                        principalSchema: "Lookup",
                        principalTable: "RestroomRequestState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RestroomRequest_Table",
                        column: x => x.TableId,
                        principalSchema: "Mealmate",
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestroomRequest_CustomerId",
                schema: "Request",
                table: "RestroomRequest",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RestroomRequest_RestroomRequestStateId",
                schema: "Request",
                table: "RestroomRequest",
                column: "RestroomRequestStateId");

            migrationBuilder.CreateIndex(
                name: "IX_RestroomRequest_TableId",
                schema: "Request",
                table: "RestroomRequest",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestroomRequest",
                schema: "Request");

            migrationBuilder.DropTable(
                name: "RestroomRequestState",
                schema: "Lookup");
        }
    }
}
