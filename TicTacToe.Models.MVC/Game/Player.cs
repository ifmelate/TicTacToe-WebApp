using TicTacToe.Models.Entity;

namespace TicTacToe.Models.MVC.Game
{
    public class Player
    {
        public int Id { get; set; }
        public Player()
        {
            GameSideEnum = GameSideEnum.Crosses;
        }
        public string Name { get; set; }
        public  GameSideEnum GameSideEnum  { get; set; }

    }
}