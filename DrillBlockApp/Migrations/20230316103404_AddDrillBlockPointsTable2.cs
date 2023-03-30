using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrillBlockApp.Migrations
{
    public partial class AddDrillBlockPointsTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrillBlocks_DrillBlockPoints_DrillBlockPointsId",
                table: "DrillBlocks");

            migrationBuilder.DropIndex(
                name: "IX_DrillBlocks_DrillBlockPointsId",
                table: "DrillBlocks");

            migrationBuilder.DropColumn(
                name: "DrillBlockPointsId",
                table: "DrillBlocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrillBlockPointsId",
                table: "DrillBlocks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrillBlocks_DrillBlockPointsId",
                table: "DrillBlocks",
                column: "DrillBlockPointsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrillBlocks_DrillBlockPoints_DrillBlockPointsId",
                table: "DrillBlocks",
                column: "DrillBlockPointsId",
                principalTable: "DrillBlockPoints",
                principalColumn: "Id");
        }
    }
}
