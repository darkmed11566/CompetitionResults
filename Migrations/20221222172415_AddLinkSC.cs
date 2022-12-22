using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddLinkSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SportsmanId",
                table: "Competitioners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Competitioners_SportsmanId",
                table: "Competitioners",
                column: "SportsmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitioners_Sportsmens_SportsmanId",
                table: "Competitioners",
                column: "SportsmanId",
                principalTable: "Sportsmens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitioners_Sportsmens_SportsmanId",
                table: "Competitioners");

            migrationBuilder.DropIndex(
                name: "IX_Competitioners_SportsmanId",
                table: "Competitioners");

            migrationBuilder.DropColumn(
                name: "SportsmanId",
                table: "Competitioners");
        }
    }
}
