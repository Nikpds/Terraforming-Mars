using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Terraforming.Api.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Invitations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Invitations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_OwnerId",
                table: "Invitations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_OwnerId",
                table: "Invitations",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_OwnerId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_OwnerId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Invitations");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Invitations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
