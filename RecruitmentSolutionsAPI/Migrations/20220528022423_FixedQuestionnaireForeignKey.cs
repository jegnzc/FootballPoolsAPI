using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentSolutionsAPI.Migrations
{
    public partial class FixedQuestionnaireForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_Candidates_CandidateId",
                table: "Questionnaires");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaires_CandidateId",
                table: "Questionnaires");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Questionnaires");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Questionnaires",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_CandidateId",
                table: "Questionnaires",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaires_Candidates_CandidateId",
                table: "Questionnaires",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
