using System.Collections.Generic;
using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class Player: EntityId
    {

        public string Name { get; set; }

        public int? UserId { get; set; }

        public virtual User User { get; set; }

        public int GameSideId { get; set; }

        public virtual GameSide GameSide { get; set; }
        public virtual ICollection<Game> WinnerGames { get; set; }
        public virtual ICollection<Game> LoserGames { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
    }
}