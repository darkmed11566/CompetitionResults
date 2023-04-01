using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompetitionResults.Migrations
{
    public partial class AddTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateWithPenaltyTrainings_Training_TrainingId",
                table: "GateWithPenaltyTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_GateWithTimeTrainings_Training_TrainingId",
                table: "GateWithTimeTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantOfTheTrainings_Training_TrainingId",
                table: "ParticipantOfTheTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_Coaches_CoachId",
                table: "Training");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Training",
                table: "Training");

            migrationBuilder.RenameTable(
                name: "Training",
                newName: "Trainings");

            migrationBuilder.RenameIndex(
                name: "IX_Training_CoachId",
                table: "Trainings",
                newName: "IX_Trainings_CoachId");

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingDate",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithPenaltyTrainings_Trainings_TrainingId",
                table: "GateWithPenaltyTrainings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithTimeTrainings_Trainings_TrainingId",
                table: "GateWithTimeTrainings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantOfTheTrainings_Trainings_TrainingId",
                table: "ParticipantOfTheTrainings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Coaches_CoachId",
                table: "Trainings",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateWithPenaltyTrainings_Trainings_TrainingId",
                table: "GateWithPenaltyTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_GateWithTimeTrainings_Trainings_TrainingId",
                table: "GateWithTimeTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantOfTheTrainings_Trainings_TrainingId",
                table: "ParticipantOfTheTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Coaches_CoachId",
                table: "Trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingDate",
                table: "Trainings");

            migrationBuilder.RenameTable(
                name: "Trainings",
                newName: "Training");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_CoachId",
                table: "Training",
                newName: "IX_Training_CoachId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Training",
                table: "Training",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithPenaltyTrainings_Training_TrainingId",
                table: "GateWithPenaltyTrainings",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GateWithTimeTrainings_Training_TrainingId",
                table: "GateWithTimeTrainings",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantOfTheTrainings_Training_TrainingId",
                table: "ParticipantOfTheTrainings",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Coaches_CoachId",
                table: "Training",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
