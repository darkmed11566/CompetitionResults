using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddLinkCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompetitionId",
                table: "Competitioners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Competitioners_CompetitionId",
                table: "Competitioners",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitioners_Competitions_CompetitionId",
                table: "Competitioners",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitioners_Competitions_CompetitionId",
                table: "Competitioners");

            migrationBuilder.DropIndex(
                name: "IX_Competitioners_CompetitionId",
                table: "Competitioners");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Competitioners");
        }
    }
}
