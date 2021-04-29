﻿using System;
using System.Collections.Generic;
using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class Game : EntityId
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int? WinnerPlayerId { get; set; }

        public virtual Player WinnerPlayer { get; set; }
        public int? LoserPlayerId { get; set; }

        public virtual Player LoserPlayer { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
    }
}