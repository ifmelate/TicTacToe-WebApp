using System.Collections.Generic;
using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class GameSide: EntityId
    {
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}