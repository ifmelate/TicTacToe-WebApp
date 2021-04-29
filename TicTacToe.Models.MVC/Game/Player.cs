using TicTacToe.Models.Entity;

namespace TicTacToe.Web.Models.Game
{
    public class Player
    {
        public string Name { get; set; }

        public virtual User User { get; set; }

        public virtual GameSide GameSide { get; set; }

    }
}