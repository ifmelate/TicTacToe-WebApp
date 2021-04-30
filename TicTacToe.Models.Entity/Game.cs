using System;
using System.Collections.Generic;
using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class Game : EntityId
    {
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Active player
        /// </summary>
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        /// <summary>
        /// Computer (PC)
        /// </summary>
        public int ComputerPlayerId { get; set; }
        public virtual Player ComputerPlayer { get; set; }
        public int? WinnerPlayerId { get; set; }

        public virtual Player WinnerPlayer { get; set; }
        public int? LoserPlayerId { get; set; }

        public virtual Player LoserPlayer { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
        public int LevelId { get; set; }
        public virtual Level Level { get; set; }
    }
}