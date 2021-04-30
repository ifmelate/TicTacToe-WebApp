using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.Data.EF.Migrations
{
    public partial class UpdateGameConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Game",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ComputerPlayerId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlayerId",
                table: "Game",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Player_PlayerId",
                table: "Game",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Player_PlayerId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PlayerId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ComputerPlayerId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Game");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Game",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
