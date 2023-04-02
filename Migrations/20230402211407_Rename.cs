using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateWithPenaltyTrainings_Trainings_TrainingId",
                table: "GateWithPenaltyTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_GateWithTimePassageTrainings_GateWithTimeTrainings_GateWithPenaltyTrainingId",
                table: "GateWithTimePassageTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_GateWithTimeTrainings_Trainings_TrainingId",
                table: "GateWithTimeTrainings");

            migrationBuilder.RenameColumn(
                name: "GateWithPenaltyTrainingId",
                table: "GateWithTimePassageTrainings",
                newName: "GateWithTimeTrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_GateWithTimePassageTrainings_GateWithPenaltyTrainingId",
                table: "GateWithTimePassageTrainings",
                newName: "IX_GateWithTimePassageTrainings_GateWithTimeTrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithPenaltyTrainings_Trainings_TrainingId",
                table: "GateWithPenaltyTrainings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithTimePassageTrainings_GateWithTimeTrainings_GateWithTimeTrainingId",
                table: "GateWithTimePassageTrainings",
                column: "GateWithTimeTrainingId",
                principalTable: "GateWithTimeTrainings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithTimeTrainings_Trainings_TrainingId",
                table: "GateWithTimeTrainings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateWithPenaltyTrainings_Trainings_TrainingId",
                table: "GateWithPenaltyTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_GateWithTimePassageTrainings_GateWithTimeTrainings_GateWithTimeTrainingId",
                table: "GateWithTimePassageTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_GateWithTimeTrainings_Trainings_TrainingId",
                table: "GateWithTimeTrainings");

            migrationBuilder.RenameColumn(
                name: "GateWithTimeTrainingId",
                table: "GateWithTimePassageTrainings",
                newName: "GateWithPenaltyTrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_GateWithTimePassageTrainings_GateWithTimeTrainingId",
                table: "GateWithTimePassageTrainings",
                newName: "IX_GateWithTimePassageTrainings_GateWithPenaltyTrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithPenaltyTrainings_Trainings_TrainingId",
                table: "GateWithPenaltyTrainings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithTimePassageTrainings_GateWithTimeTrainings_GateWithPenaltyTrainingId",
                table: "GateWithTimePassageTrainings",
                column: "GateWithPenaltyTrainingId",
                principalTable: "GateWithTimeTrainings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithTimeTrainings_Trainings_TrainingId",
                table: "GateWithTimeTrainings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
