using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRated = table.Column<bool>(type: "bit", nullable: false),
                    IsFull = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Judges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sportsmens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    Rang = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URLPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Achievements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sportsmens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackType = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    IsFull = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitioners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    BoatClass = table.Column<int>(type: "int", nullable: false),
                    StatusInTrack = table.Column<int>(type: "int", nullable: false),
                    SportsmanId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitioners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitioners_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competitioners_Sportsmens_SportsmanId",
                        column: x => x.SportsmanId,
                        principalTable: "Sportsmens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GateWithTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    GateName = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithTimes_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    IsFull = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GateWithTimePassages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatePasssage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GateWihtTimeId = table.Column<int>(type: "int", nullable: false),
                    CompetitionerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithTimePassages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithTimePassages_Competitioners_CompetitionerId",
                        column: x => x.CompetitionerId,
                        principalTable: "Competitioners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GateWithTimePassages_GateWithTimes_GateWihtTimeId",
                        column: x => x.GateWihtTimeId,
                        principalTable: "GateWithTimes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GateWithPenalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GateNumber = table.Column<int>(type: "int", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithPenalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithPenalties_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GateWithPenaltiePassages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GateWihtPenaltyId = table.Column<int>(type: "int", nullable: false),
                    PenaltyOnGate = table.Column<int>(type: "int", nullable: false),
                    CompetitionerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithPenaltiePassages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithPenaltiePassages_Competitioners_CompetitionerId",
                        column: x => x.CompetitionerId,
                        principalTable: "Competitioners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GateWithPenaltiePassages_GateWithPenalties_GateWihtPenaltyId",
                        column: x => x.GateWihtPenaltyId,
                        principalTable: "GateWithPenalties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitioners_CompetitionId",
                table: "Competitioners",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitioners_SportsmanId",
                table: "Competitioners",
                column: "SportsmanId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithPenaltiePassages_CompetitionerId",
                table: "GateWithPenaltiePassages",
                column: "CompetitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithPenaltiePassages_GateWihtPenaltyId",
                table: "GateWithPenaltiePassages",
                column: "GateWihtPenaltyId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithPenalties_SectorId",
                table: "GateWithPenalties",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithTimePassages_CompetitionerId",
                table: "GateWithTimePassages",
                column: "CompetitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithTimePassages_GateWihtTimeId",
                table: "GateWithTimePassages",
                column: "GateWihtTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithTimes_TrackId",
                table: "GateWithTimes",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_TrackId",
                table: "Sectors",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_CompetitionId",
                table: "Tracks",
                column: "CompetitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateWithPenaltiePassages");

            migrationBuilder.DropTable(
                name: "GateWithTimePassages");

            migrationBuilder.DropTable(
                name: "Judges");

            migrationBuilder.DropTable(
                name: "GateWithPenalties");

            migrationBuilder.DropTable(
                name: "Competitioners");

            migrationBuilder.DropTable(
                name: "GateWithTimes");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Sportsmens");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Competitions");
        }
    }
}
