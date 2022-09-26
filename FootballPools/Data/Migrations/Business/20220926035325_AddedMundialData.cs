using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballPools.Data.Migrations.Business
{
    public partial class AddedMundialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_LeagueType_LeagueTypeId",
                table: "Leagues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeagueType",
                table: "LeagueType");

            migrationBuilder.RenameTable(
                name: "LeagueType",
                newName: "LeagueTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeagueTypes",
                table: "LeagueTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matchs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstParticipantId = table.Column<int>(type: "int", nullable: true),
                    SecondParticipantId = table.Column<int>(type: "int", nullable: true),
                    WinnerId = table.Column<int>(type: "int", nullable: true),
                    FirstParticipantScore = table.Column<int>(type: "int", nullable: true),
                    SecondParticipantScore = table.Column<int>(type: "int", nullable: true),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StadiumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matchs_Participants_FirstParticipantId",
                        column: x => x.FirstParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matchs_Participants_SecondParticipantId",
                        column: x => x.SecondParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matchs_Participants_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Participants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matchs_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueMemberPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstParticipantScore = table.Column<int>(type: "int", nullable: false),
                    SecondParticipantScore = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    LeagueMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueMemberPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueMemberPredictions_LeagueMembers_LeagueMemberId",
                        column: x => x.LeagueMemberId,
                        principalTable: "LeagueMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueMemberPredictions_Matchs_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TournamentId",
                table: "Groups",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMemberPredictions_LeagueMemberId",
                table: "LeagueMemberPredictions",
                column: "LeagueMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMemberPredictions_MatchId",
                table: "LeagueMemberPredictions",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_FirstParticipantId",
                table: "Matchs",
                column: "FirstParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_SecondParticipantId",
                table: "Matchs",
                column: "SecondParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_StadiumId",
                table: "Matchs",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_WinnerId",
                table: "Matchs",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_TournamentId",
                table: "Participants",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_LeagueTypes_LeagueTypeId",
                table: "Leagues",
                column: "LeagueTypeId",
                principalTable: "LeagueTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_LeagueTypes_LeagueTypeId",
                table: "Leagues");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "LeagueMemberPredictions");

            migrationBuilder.DropTable(
                name: "Matchs");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeagueTypes",
                table: "LeagueTypes");

            migrationBuilder.RenameTable(
                name: "LeagueTypes",
                newName: "LeagueType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeagueType",
                table: "LeagueType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_LeagueType_LeagueTypeId",
                table: "Leagues",
                column: "LeagueTypeId",
                principalTable: "LeagueType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
