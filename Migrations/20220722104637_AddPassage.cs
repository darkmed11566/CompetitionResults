
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddPassage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GateWithPenaltiePassages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PenaltyOnGate = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithPenaltiePassages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GateWithTimePassages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatePasssage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithTimePassages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateWithPenaltiePassages");

            migrationBuilder.DropTable(
                name: "GateWithTimePassages");
        }
    }
}
