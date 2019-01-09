using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Terraforming.Api.Migrations
{
    public partial class teams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamUsers");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "TeamUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TeamUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeamId",
                table: "TeamUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamUsers",
                table: "TeamUsers",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Teams_TeamId",
                table: "TeamUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Users_UserId",
                table: "TeamUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<string>(
                name: "TeamId",
                table: "TeamUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TeamUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "TeamUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "TeamUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamUsers",
                table: "TeamUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_UserId",
                table: "TeamUsers",
                column: "UserId");

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
    }
}
