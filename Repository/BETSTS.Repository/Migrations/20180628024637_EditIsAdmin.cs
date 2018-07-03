using Microsoft.EntityFrameworkCore.Migrations;

namespace BETSTS.Repository.Migrations
{
    public partial class EditIsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsAdmin",
                table: "User",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsAdmin",
                table: "User",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
