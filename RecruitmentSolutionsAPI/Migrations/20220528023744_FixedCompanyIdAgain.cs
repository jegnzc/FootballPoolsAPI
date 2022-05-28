using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentSolutionsAPI.Migrations
{
    public partial class FixedCompanyIdAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pipeline_Companies_CompanyId1",
                table: "Pipeline");

            migrationBuilder.DropIndex(
                name: "IX_Pipeline_CompanyId1",
                table: "Pipeline");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "Pipeline");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Pipeline",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pipeline_CompanyId",
                table: "Pipeline",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pipeline_Companies_CompanyId",
                table: "Pipeline",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pipeline_Companies_CompanyId",
                table: "Pipeline");

            migrationBuilder.DropIndex(
                name: "IX_Pipeline_CompanyId",
                table: "Pipeline");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "Pipeline",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId1",
                table: "Pipeline",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pipeline_CompanyId1",
                table: "Pipeline",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pipeline_Companies_CompanyId1",
                table: "Pipeline",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
