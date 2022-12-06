using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddLinkSG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFull",
                table: "Sectors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "SectorId",
                table: "GateWithPenalties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_GateWithPenalties_SectorId",
                table: "GateWithPenalties",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithPenalties_Sectors_SectorId",
                table: "GateWithPenalties",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateWithPenalties_Sectors_SectorId",
                table: "GateWithPenalties");

            migrationBuilder.DropIndex(
                name: "IX_GateWithPenalties_SectorId",
                table: "GateWithPenalties");

            migrationBuilder.DropColumn(
                name: "IsFull",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "GateWithPenalties");
        }
    }
}
