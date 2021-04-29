using TicTacToe.Models.Entity.BaseEntityModel;

namespace TicTacToe.Models.Entity
{
    public class Move: EntityId
    {
        public int MoveNumber { get; set; }

        public int CellId { get; set; }
        public virtual Cell Cell { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}