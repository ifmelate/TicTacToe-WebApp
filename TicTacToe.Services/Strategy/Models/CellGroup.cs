using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.MVC.Game;

namespace TicTacToe.Services.Strategy.Models
{
    public class CellGroup
    {
        public Direction Direction { get; set; }

        public List<GameCell> GameCells { get; set; }

        public List<GameCell> AvailableGameCells
        {
            get { return GameCells.Where(d => d.GameSide == null).ToList(); }
        }

        public List<GameCell> OccupiedGameCells
        {
            get { return GameCells.Where(d => d.GameSide != null).ToList(); }
        }
    }
}