using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class UserAllergenAndDietary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAllergen",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    AllergenId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAllergen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAllergen_Allergen_AllergenId",
                        column: x => x.AllergenId,
                        principalSchema: "Lookup",
                        principalTable: "Allergen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAllergen_User",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserDietary",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()"),
                    DietaryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDietary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDietary_Dietary_DietaryId",
                        column: x => x.DietaryId,
                        principalSchema: "Lookup",
                        principalTable: "Dietary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDietary_User",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergen_AllergenId",
                schema: "Identity",
                table: "UserAllergen",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergen_UserId",
                schema: "Identity",
                table: "UserAllergen",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDietary_DietaryId",
                schema: "Identity",
                table: "UserDietary",
                column: "DietaryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDietary_UserId",
                schema: "Identity",
                table: "UserDietary",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAllergen",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserDietary",
                schema: "Identity");
        }
    }
}
