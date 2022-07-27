using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddDependencyCompetitionTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NameOfCompetitionId",
                table: "Tracks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_NameOfCompetitionId",
                table: "Tracks",
                column: "NameOfCompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Competitions_NameOfCompetitionId",
                table: "Tracks",
                column: "NameOfCompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Competitions_NameOfCompetitionId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_NameOfCompetitionId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "NameOfCompetitionId",
                table: "Tracks");
        }
    }
}
