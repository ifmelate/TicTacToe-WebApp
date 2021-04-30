using TicTacToe.Models.Entity;

namespace TicTacToe.Models.MVC.Game
{
    public class Player
    {
        public int Id { get; set; }
        public Player()
        {
            GameSide = GameSideEnum.Crosses;
        }
        public string Name { get; set; }

        public  GameSideEnum GameSide  { get; set; }

    }
}