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
            if (emptyCells.Length == cells.Length)
            {
                var cellNumber = new Random().Next(0, cornerCells.Count() - 1);
                return cornerCells[cellNumber];
            }
            else
            {

                var firstDiagonalNames = new[] { "1.1", "2.2", "3.3" }; 
                var secondDiagonalNames = new[] { "3.1", "2.2", "1.3" };
                var cellGroups = new List<CellGroup>
                {
                    new CellGroup
                    {
                        GameCells = cells.Where(d => d.Cell.Name.Contains("1.")).ToList(),
                        Direction = Direction.FirstRow
                    },
                    new CellGroup
                    {
                        GameCells = cells.Where(d => d.Cell.Name.Contains("2.")).ToList(),
                        Direction = Direction.SecondRow
                    },
                    new CellGroup
                    {
                        GameCells = cells.Where(d => d.Cell.Name.Contains("3.")).ToList(),
                        Direction = Direction.ThirdRow
                    },
                    new CellGroup
                    {
                        GameCells = cells.Where(d => d.Cell.Name.Contains(".1")).ToList(),
                        Direction = Direction.FirstColumn
                    },
                    new CellGroup
                    {
                        GameCells = cells.Where(d => d.Cell.Name.Contains(".2")).ToList(),
                        Direction = Direction.SecondColumn
                    },
                    new CellGroup
                    {
                        GameCells = cells.Where(d => d.Cell.Name.Contains(".3")).ToList(),
                        Direction = Direction.ThirdColumn
                    },
                    new CellGroup
                    {
                        GameCells = cells.Where(d => firstDiagonalNames.Contains(d.Cell.Name)).ToList(),
                        Direction = Direction.FirstDiagonal
                    },
                    new CellGroup
                    {
                        GameCells = cells.Where(d => secondDiagonalNames.Contains(d.Cell.Name)).ToList(),
                        Direction = Direction.SecondDiagonal
                    }
                };


                var dangerDirection = cellGroups.FirstOrDefault(d => d.OccupiedGameCells.Count(s=>s.GameSide != computerGameSide) == 2 && d.AvailableGameCells.Count > 0);
                if (dangerDirection != null)
                    return dangerDirection.AvailableGameCells.First();


                var directionNumber = new Random().Next(0, 7);
                var cellGroup = cellGroups.FirstOrDefault(d => d.Direction == (Direction) directionNumber);
                if (cellGroup.AvailableGameCells.Count() == 1)
                    return cellGroup.AvailableGameCells[0];

                var cellNumber = new Random().Next(0, cellGroup.AvailableGameCells.Count-1);
                return cellGroup.AvailableGameCells.ToArray()[cellNumber];
            }
        }
    }
}