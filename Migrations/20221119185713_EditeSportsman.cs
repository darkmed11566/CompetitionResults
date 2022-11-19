using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class EditeSportsman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Sportsmens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusInTrack",
                table: "Competitioners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Sportsmens");

            migrationBuilder.DropColumn(
                name: "StatusInTrack",
                table: "Competitioners");
        }
    }
}
