using TicTacToe.Models.Entity;

namespace TicTacToe.Models.MVC.Game
{
    public class GameCell
    {
        public int CellId { get; set; }
        
        public Cell Cell { get; set; }
        public GameSideEnum? GameSide { get; set; }

        public int GameId { get; set; }

    }
}