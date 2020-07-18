using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class AddOptionAllergenDietary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionItemAllergen",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionItemId = table.Column<int>(type: "INT", nullable: false),
                    AllergenId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionItemAllergen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionItemAllergen_Allergen_AllergenId",
                        column: x => x.AllergenId,
                        principalSchema: "Lookup",
                        principalTable: "Allergen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionItemAllergen_OptionItem",
                        column: x => x.OptionItemId,
                        principalSchema: "Lookup",
                        principalTable: "OptionItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OptionItemDietary",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionItemId = table.Column<int>(type: "INT", nullable: false),
                    DietaryId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionItemDietary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionItemDietary_Dietary_DietaryId",
                        column: x => x.DietaryId,
                        principalSchema: "Lookup",
                        principalTable: "Dietary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionItemDietary_OptionItem",
                        column: x => x.OptionItemId,
                        principalSchema: "Lookup",
                        principalTable: "OptionItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionItemAllergen_AllergenId",
                schema: "Lookup",
                table: "OptionItemAllergen",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionItemAllergen_OptionItemId",
                schema: "Lookup",
                table: "OptionItemAllergen",
                column: "OptionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionItemDietary_DietaryId",
                schema: "Lookup",
                table: "OptionItemDietary",
                column: "DietaryId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionItemDietary_OptionItemId",
                schema: "Lookup",
                table: "OptionItemDietary",
                column: "OptionItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionItemAllergen",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "OptionItemDietary",
                schema: "Lookup");
        }
    }
}
