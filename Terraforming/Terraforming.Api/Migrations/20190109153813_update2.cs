using Microsoft.EntityFrameworkCore.Migrations;

namespace Terraforming.Api.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamsUsers_Teams_TeamId",
                table: "TeamsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsUsers_Users_UserId",
                table: "TeamsUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamsUsers",
                table: "TeamsUsers");

            migrationBuilder.RenameTable(
                name: "TeamsUsers",
                newName: "TeamUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TeamsUsers_UserId",
                table: "TeamUsers",
                newName: "IX_TeamUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamsUsers_TeamId",
                table: "TeamUsers",
                newName: "IX_TeamUsers_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamUsers",
                table: "TeamUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Teams_TeamId",
                table: "TeamUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Users_UserId",
                table: "TeamUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Teams_TeamId",
                table: "TeamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Users_UserId",
                table: "TeamUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamUsers",
                table: "TeamUsers");

            migrationBuilder.RenameTable(
                name: "TeamUsers",
                newName: "TeamsUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamsUsers",
                newName: "IX_TeamsUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUsers_TeamId",
                table: "TeamsUsers",
                newName: "IX_TeamsUsers_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamsUsers",
                table: "TeamsUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsUsers_Teams_TeamId",
                table: "TeamsUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsUsers_Users_UserId",
                table: "TeamsUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
