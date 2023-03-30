using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DrillBlockApp.Migrations
{
    public partial class AddDrillBlockPointsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrillBlockPointsId",
                table: "DrillBlocks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrillBlockPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DrillBlockId = table.Column<int>(type: "integer", nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Z = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrillBlockPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrillBlockPoints_DrillBlocks_DrillBlockId",
                        column: x => x.DrillBlockId,
                        principalTable: "DrillBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrillBlocks_DrillBlockPointsId",
                table: "DrillBlocks",
                column: "DrillBlockPointsId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillBlockPoints_DrillBlockId",
                table: "DrillBlockPoints",
                column: "DrillBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrillBlocks_DrillBlockPoints_DrillBlockPointsId",
                table: "DrillBlocks",
                column: "DrillBlockPointsId",
                principalTable: "DrillBlockPoints",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrillBlocks_DrillBlockPoints_DrillBlockPointsId",
                table: "DrillBlocks");

            migrationBuilder.DropTable(
                name: "DrillBlockPoints");

            migrationBuilder.DropIndex(
                name: "IX_DrillBlocks_DrillBlockPointsId",
                table: "DrillBlocks");

            migrationBuilder.DropColumn(
                name: "DrillBlockPointsId",
                table: "DrillBlocks");
        }
    }
}
