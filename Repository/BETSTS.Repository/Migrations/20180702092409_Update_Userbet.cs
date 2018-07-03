using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BETSTS.Repository.Migrations
{
    public partial class Update_Userbet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchTeam_FootballTeam_FootballTeamId",
                table: "MatchTeam");

            migrationBuilder.DropIndex(
                name: "IX_MatchTeam_FootballTeamId",
                table: "MatchTeam");

            migrationBuilder.DropColumn(
                name: "FootballTeamId",
                table: "MatchTeam");

            migrationBuilder.AddColumn<decimal>(
                name: "MoneyLostLast",
                table: "UserBet",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_TeamId",
                table: "MatchTeam",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchTeam_FootballTeam_TeamId",
                table: "MatchTeam",
                column: "TeamId",
                principalTable: "FootballTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchTeam_FootballTeam_TeamId",
                table: "MatchTeam");

            migrationBuilder.DropIndex(
                name: "IX_MatchTeam_TeamId",
                table: "MatchTeam");

            migrationBuilder.DropColumn(
                name: "MoneyLostLast",
                table: "UserBet");

            migrationBuilder.AddColumn<Guid>(
                name: "FootballTeamId",
                table: "MatchTeam",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_FootballTeamId",
                table: "MatchTeam",
                column: "FootballTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchTeam_FootballTeam_FootballTeamId",
                table: "MatchTeam",
                column: "FootballTeamId",
                principalTable: "FootballTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
