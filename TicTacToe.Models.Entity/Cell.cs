using System.Collections.Generic;
using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class Cell: EntityId
    {
        public string Name { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
    }
}