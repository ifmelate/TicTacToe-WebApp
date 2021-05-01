using System;

namespace TicTacToe.Models.MVC.Results
{
    public class GameResult
    {
        public int GameId { get; set; }
        public Models.MVC.Game.Player Player { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public Entity.Player WinnerPlayer { get; set; }
    }
}