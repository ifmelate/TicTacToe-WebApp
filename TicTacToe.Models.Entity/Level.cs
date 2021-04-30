using System.Collections;
using System.Collections.Generic;
using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class Level:EntityId
    {
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        
    }
}