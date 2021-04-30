using System;
using System.Collections.Generic;

namespace TicTacToe.Models.MVC.Game
{
    public class Game
    {
        public int Id { get; set; }
        public virtual Player Player { get; set; }
        public DateTime? StartDateTime { get; set; }
        public IList<GameCell> GameCells { get; set; }
        public Level Level { get; set; }
    }
}