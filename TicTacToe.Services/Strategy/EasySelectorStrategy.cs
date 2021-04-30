using System;
using System.Linq;
using TicTacToe.Models.MVC.Game;

namespace TicTacToe.Services.Strategy
{
    /// <summary>
    /// Return random cell
    /// </summary>
    internal class EasySelectorStrategy : ISelectorStrategy
    {
        public GameCell GetCell(GameCell[] cells)
        {
            var emptyCells = cells.Where(d => d.GameSide == null).ToArray();

            if (emptyCells.Count() == 1)
                return emptyCells[0];
            var cellNumber = new Random().Next(0, emptyCells.Count() - 1);

            return emptyCells[cellNumber];
        }
    }
}