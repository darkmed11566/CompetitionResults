using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddCoach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachName",
                table: "Sportsmens");

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Sportsmens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sportsmens_CoachId",
                table: "Sportsmens",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sportsmens_Coaches_CoachId",
                table: "Sportsmens",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sportsmens_Coaches_CoachId",
                table: "Sportsmens");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Sportsmens_CoachId",
                table: "Sportsmens");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Sportsmens");

            migrationBuilder.AddColumn<string>(
                name: "CoachName",
                table: "Sportsmens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
