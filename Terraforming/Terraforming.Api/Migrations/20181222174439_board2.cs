using Microsoft.EntityFrameworkCore.Migrations;

namespace Terraforming.Api.Migrations
{
    public partial class board2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Board",
                table: "GameScore",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Board",
                table: "GameScore",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
