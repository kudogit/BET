using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BETSTS.Repository.Migrations
{
    public partial class addold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchange",
                columns: table => new
                {
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true),
                    Sent = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    AmountId = table.Column<Guid>(nullable: false),
                    AmoutId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchange_Amout_AmoutId",
                        column: x => x.AmoutId,
                        principalTable: "Amout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_AmoutId",
                table: "Exchange",
                column: "AmoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchange");
        }
    }
}
