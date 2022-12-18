using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddLinkTG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TrackId",
                table: "GateWithTimes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_GateWithTimes_TrackId",
                table: "GateWithTimes",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithTimes_Tracks_TrackId",
                table: "GateWithTimes",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateWithTimes_Tracks_TrackId",
                table: "GateWithTimes");

            migrationBuilder.DropIndex(
                name: "IX_GateWithTimes_TrackId",
                table: "GateWithTimes");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "GateWithTimes");
        }
    }
}
