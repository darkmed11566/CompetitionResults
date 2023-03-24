using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddTrainingMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GateWithPenaltyTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GateNumber = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithPenaltyTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithPenaltyTrainings_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GateWithTimeTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    GateName = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithTimeTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithTimeTrainings_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantOfTheTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoatClass = table.Column<int>(type: "int", nullable: false),
                    StatusInTrack = table.Column<int>(type: "int", nullable: false),
                    SportsmanId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantOfTheTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantOfTheTrainings_Sportsmens_SportsmanId",
                        column: x => x.SportsmanId,
                        principalTable: "Sportsmens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantOfTheTrainings_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GateWithPenaltyPassageTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GateWihtPenaltyTrainingId = table.Column<int>(type: "int", nullable: false),
                    PenaltyOnGate = table.Column<int>(type: "int", nullable: false),
                    ParticipantOfTheTrainingId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithPenaltyPassageTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithPenaltyPassageTrainings_GateWithPenaltyTrainings_GateWihtPenaltyTrainingId",
                        column: x => x.GateWihtPenaltyTrainingId,
                        principalTable: "GateWithPenaltyTrainings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GateWithPenaltyPassageTrainings_ParticipantOfTheTrainings_ParticipantOfTheTrainingId",
                        column: x => x.ParticipantOfTheTrainingId,
                        principalTable: "ParticipantOfTheTrainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GateWithTimePassageTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatePasssage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GateWithPenaltyTrainingId = table.Column<int>(type: "int", nullable: false),
                    ParticipantOfTheTrainingId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateWithTimePassageTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateWithTimePassageTrainings_GateWithTimeTrainings_GateWithPenaltyTrainingId",
                        column: x => x.GateWithPenaltyTrainingId,
                        principalTable: "GateWithTimeTrainings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GateWithTimePassageTrainings_ParticipantOfTheTrainings_ParticipantOfTheTrainingId",
                        column: x => x.ParticipantOfTheTrainingId,
                        principalTable: "ParticipantOfTheTrainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GateWithPenaltyPassageTrainings_GateWihtPenaltyTrainingId",
                table: "GateWithPenaltyPassageTrainings",
                column: "GateWihtPenaltyTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithPenaltyPassageTrainings_ParticipantOfTheTrainingId",
                table: "GateWithPenaltyPassageTrainings",
                column: "ParticipantOfTheTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithPenaltyTrainings_TrainingId",
                table: "GateWithPenaltyTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithTimePassageTrainings_GateWithPenaltyTrainingId",
                table: "GateWithTimePassageTrainings",
                column: "GateWithPenaltyTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithTimePassageTrainings_ParticipantOfTheTrainingId",
                table: "GateWithTimePassageTrainings",
                column: "ParticipantOfTheTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_GateWithTimeTrainings_TrainingId",
                table: "GateWithTimeTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantOfTheTrainings_SportsmanId",
                table: "ParticipantOfTheTrainings",
                column: "SportsmanId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantOfTheTrainings_TrainingId",
                table: "ParticipantOfTheTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_CoachId",
                table: "Training",
                column: "CoachId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateWithPenaltyPassageTrainings");

            migrationBuilder.DropTable(
                name: "GateWithTimePassageTrainings");

            migrationBuilder.DropTable(
                name: "GateWithPenaltyTrainings");

            migrationBuilder.DropTable(
                name: "GateWithTimeTrainings");

            migrationBuilder.DropTable(
                name: "ParticipantOfTheTrainings");

            migrationBuilder.DropTable(
                name: "Training");
        }
    }
}
