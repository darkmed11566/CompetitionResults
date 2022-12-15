using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddLinkCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompetitionId",
                table: "Tracks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsFull",
                table: "Tracks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFull",
                table: "Competitions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_CompetitionId",
                table: "Tracks",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Competitions_CompetitionId",
                table: "Tracks",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Competitions_CompetitionId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_CompetitionId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "IsFull",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "IsFull",
                table: "Competitions");
        }
    }
}
