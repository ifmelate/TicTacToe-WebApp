using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.Data.EF.Migrations
{
    public partial class CreateInitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cell",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cell", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameSide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSide", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    GameSideId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_GameSide_GameSideId",
                        column: x => x.GameSideId,
                        principalTable: "GameSide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WinnerPlayerId = table.Column<int>(type: "int", nullable: true),
                    LoserPlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Player_LoserPlayerId",
                        column: x => x.LoserPlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_Player_WinnerPlayerId",
                        column: x => x.WinnerPlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveNumber = table.Column<int>(type: "int", nullable: false),
                    CellId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Move_Cell_CellId",
                        column: x => x.CellId,
                        principalTable: "Cell",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Move_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Move_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cell",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "1.1" },
                    { 2, "1.2" },
                    { 3, "1.3" },
                    { 4, "2.1" },
                    { 5, "2.2" },
                    { 6, "2.3" },
                    { 7, "3.1" },
                    { 8, "3.2" },
                    { 9, "3.3" }
                });

            migrationBuilder.InsertData(
                table: "GameSide",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Crosses" },
                    { 2, "Zeros" }
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "GameSideId", "Name", "UserId" },
                values: new object[] { 1, 1, "PC", null });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "GameSideId", "Name", "UserId" },
                values: new object[] { 2, 2, "PC", null });

            migrationBuilder.CreateIndex(
                name: "IX_Game_LoserPlayerId",
                table: "Game",
                column: "LoserPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_WinnerPlayerId",
                table: "Game",
                column: "WinnerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_CellId",
                table: "Move",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_GameId",
                table: "Move",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_PlayerId",
                table: "Move",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameSideId",
                table: "Player",
                column: "GameSideId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_UserId",
                table: "Player",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "Cell");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "GameSide");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
