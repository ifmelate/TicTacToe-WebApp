using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Services.Strategy.Models;

namespace TicTacToe.Services.Strategy
{
    /// <summary>
    /// 
    /// </summary>
    internal class HardSelectorStrategy : ISelectorStrategy
    {
        public GameCell GetCell(GameCell[] cells, GameSideEnum computerGameSide)
        {
            var emptyCells = cells.Where(d => d.GameSide == null).ToArray();
            var cornerNames = new[] { "1.1", "1.3", "3.1", "3.3" };

            var cornerCells = cells.Where(d => cornerNames.Contains(d.Cell.Name)).ToArray();
            int cellNumber;
            if (emptyCells.Length == cells.Length)
            {
                cellNumber = new Random().Next(0, cornerCells.Count() - 1);
                return cornerCells[cellNumber];
            }

            var cellGroups = CellGroupsHelper.ExecuteStructuredGroups(cells);
            var dangerDirection = cellGroups.FirstOrDefault(d => d.OccupiedGameCells.Count(s => s.GameSide != computerGameSide) == 2 && d.AvailableGameCells.Count > 0);
            if (dangerDirection != null)
                return dangerDirection.AvailableGameCells.First();


            var directionNumber = new Random().Next(0, 7);
            var cellGroup = cellGroups.FirstOrDefault(d => d.Direction == (Direction)directionNumber);
            if (cellGroup.AvailableGameCells.Count() == 1)
                return cellGroup.AvailableGameCells[0];

            cellNumber = new Random().Next(0, cellGroup.AvailableGameCells.Count - 1);
            return cellGroup.AvailableGameCells.ToArray()[cellNumber];
        }
    }
}
