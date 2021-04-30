using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.Data.EF.Migrations
{
    public partial class AddRelationComputerPlayerInGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Game_ComputerPlayerId",
                table: "Game",
                column: "ComputerPlayerId");


            migrationBuilder.AddForeignKey(
                name: "FK_Game_Player_ComputerPlayerId",
                table: "Game",
                column: "ComputerPlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Player_ComputerPlayerId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropIndex(
                name: "IX_Game_ComputerPlayerId",
                table: "Game");


        }
    }
}
