using System;
using System.Linq;
using TicTacToe.Models.MVC.Game;

namespace TicTacToe.Services.Strategy
{
    /// <summary>
    /// 
    /// </summary>
    internal class HardSelectorStrategy : ISelectorStrategy
    {
        public GameCell GetCell(GameCell[] cells)
        {
            var emptyCells = cells.Where(d => d.GameSide == null).ToArray();
            var cornerNames = new[] {"1.1", "1.3", "3.1", "3.3"};
            var cornerCells = cells.Where(d => cornerNames.Contains(d.Cell.Name)).ToArray();
            if (emptyCells.Length == cells.Length)
            {
                var cellNumber = new Random().Next(0, cornerCells.Count() - 1);
                return cornerCells[cellNumber];
            }
            else
            {
                
            }
        }
    }
}