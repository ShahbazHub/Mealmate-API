using Microsoft.EntityFrameworkCore.Migrations;

namespace Mealmate.Api.Migrations
{
    public partial class BranchOptionItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                schema: "Lookup",
                table: "OptionItem",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OptionItem_BranchId",
                schema: "Lookup",
                table: "OptionItem",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionItem_Branch",
                schema: "Lookup",
                table: "OptionItem",
                column: "BranchId",
                principalSchema: "Mealmate",
                principalTable: "Branch",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionItem_Branch",
                schema: "Lookup",
                table: "OptionItem");

            migrationBuilder.DropIndex(
                name: "IX_OptionItem_BranchId",
                schema: "Lookup",
                table: "OptionItem");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "Lookup",
                table: "OptionItem");
        }
    }
}
