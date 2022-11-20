using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddLinkTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NamesOfCoaches",
                table: "Sportsmens",
                newName: "CoachName");

            migrationBuilder.RenameColumn(
                name: "NameOfClub",
                table: "Sportsmens",
                newName: "ClubName");

            migrationBuilder.RenameColumn(
                name: "SectorNumber",
                table: "Sectors",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Competitions",
                newName: "IsRated");

            migrationBuilder.RenameColumn(
                name: "CompetitionStartData",
                table: "Competitions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "CompetitionName",
                table: "Competitions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CompetitionEndtData",
                table: "Competitions",
                newName: "EndtDate");

            migrationBuilder.RenameColumn(
                name: "Class",
                table: "Competitioners",
                newName: "BoatClass");

            migrationBuilder.AddColumn<long>(
                name: "TrackId",
                table: "Sectors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_TrackId",
                table: "Sectors",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sectors_Tracks_TrackId",
                table: "Sectors",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sectors_Tracks_TrackId",
                table: "Sectors");

            migrationBuilder.DropIndex(
                name: "IX_Sectors_TrackId",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Sectors");

            migrationBuilder.RenameColumn(
                name: "CoachName",
                table: "Sportsmens",
                newName: "NamesOfCoaches");

            migrationBuilder.RenameColumn(
                name: "ClubName",
                table: "Sportsmens",
                newName: "NameOfClub");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Sectors",
                newName: "SectorNumber");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Competitions",
                newName: "CompetitionStartData");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Competitions",
                newName: "CompetitionName");

            migrationBuilder.RenameColumn(
                name: "IsRated",
                table: "Competitions",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "EndtDate",
                table: "Competitions",
                newName: "CompetitionEndtData");

            migrationBuilder.RenameColumn(
                name: "BoatClass",
                table: "Competitioners",
                newName: "Class");
        }
    }
}
