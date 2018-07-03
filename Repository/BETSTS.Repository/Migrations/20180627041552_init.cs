using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BETSTS.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballTeam",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Coach = table.Column<string>(nullable: true),
                    Point = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballTeam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceConfiguration",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleConfiguration",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    Lost = table.Column<decimal>(nullable: false),
                    Draw = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PasswordLastUpdatedTime = table.Column<DateTimeOffset>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhoneConfirmedTime = table.Column<DateTimeOffset>(nullable: true),
                    OTP = table.Column<string>(nullable: true),
                    OTPExpireTime = table.Column<DateTimeOffset>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmedTime = table.Column<DateTimeOffset>(nullable: true),
                    ConfirmEmailToken = table.Column<string>(nullable: true),
                    ConfirmEmailTokenExpireTime = table.Column<DateTimeOffset>(nullable: true),
                    SetPasswordToken = table.Column<string>(nullable: true),
                    SetPasswordTokenExpireTime = table.Column<DateTimeOffset>(nullable: true),
                    BannedTime = table.Column<DateTimeOffset>(nullable: true),
                    BannedRemark = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    TimeMatch = table.Column<DateTime>(nullable: false),
                    Stadium = table.Column<string>(nullable: true),
                    PriceConfigId = table.Column<Guid>(nullable: false),
                    IsUpdated = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_PriceConfiguration_PriceConfigId",
                        column: x => x.PriceConfigId,
                        principalTable: "PriceConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Amout",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    Sent = table.Column<decimal>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amout_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    ExpireOn = table.Column<DateTimeOffset>(nullable: true),
                    TotalUsage = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MarkerName = table.Column<string>(nullable: true),
                    MarkerVersion = table.Column<string>(nullable: true),
                    OsName = table.Column<string>(nullable: true),
                    OsVersion = table.Column<string>(nullable: true),
                    EngineName = table.Column<string>(nullable: true),
                    EngineVersion = table.Column<string>(nullable: true),
                    BrowserName = table.Column<string>(nullable: true),
                    BrowserVersion = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(nullable: true),
                    CountryIsoCode = table.Column<string>(nullable: true),
                    ContinentCode = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    AccuracyRadius = table.Column<int>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    DeviceHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    Goals = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    FootballTeamId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeam_FootballTeam_FootballTeamId",
                        column: x => x.FootballTeamId,
                        principalTable: "FootballTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTeam_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBet",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false),
                    TeamWinId = table.Column<Guid>(nullable: false),
                    IsUpdated = table.Column<int>(nullable: false),
                    MoneyLost = table.Column<decimal>(nullable: false),
                    TimeBet = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBet_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amout_DeletedTime",
                table: "Amout",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Amout_Id",
                table: "Amout",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Amout_UserId",
                table: "Amout",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FootballTeam_DeletedTime",
                table: "FootballTeam",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_FootballTeam_Id",
                table: "FootballTeam",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_DeletedTime",
                table: "Match",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Id",
                table: "Match",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PriceConfigId",
                table: "Match",
                column: "PriceConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_DeletedTime",
                table: "MatchTeam",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_FootballTeamId",
                table: "MatchTeam",
                column: "FootballTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_Id",
                table: "MatchTeam",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_MatchId",
                table: "MatchTeam",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceConfiguration_DeletedTime",
                table: "PriceConfiguration",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PriceConfiguration_Id",
                table: "PriceConfiguration",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleConfiguration_DeletedTime",
                table: "RuleConfiguration",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_RuleConfiguration_Id",
                table: "RuleConfiguration",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_DeletedTime",
                table: "User",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_OTP",
                table: "User",
                column: "OTP");

            migrationBuilder.CreateIndex(
                name: "IX_User_Phone",
                table: "User",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserBet_DeletedTime",
                table: "UserBet",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserBet_Id",
                table: "UserBet",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBet_MatchId",
                table: "UserBet",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBet_UserId",
                table: "UserBet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_DeletedTime",
                table: "UserProfile",
                column: "DeletedTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Id",
                table: "UserProfile",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amout");

            migrationBuilder.DropTable(
                name: "MatchTeam");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RuleConfiguration");

            migrationBuilder.DropTable(
                name: "UserBet");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "FootballTeam");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "PriceConfiguration");
        }
    }
}
