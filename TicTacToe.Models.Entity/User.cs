using System;
using System.Collections.Generic;
using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class User: EntityId
    {

        public string Ip { get; set; }

        public DateTime EntryDate { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}