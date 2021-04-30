using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Services.Strategy.Models;

namespace TicTacToe.Services
{
    /// <summary>
    /// Understandable structure of Cells by Directions (row, column, diagonal)
    /// </summary>
    public static class CellGroupsHelper
    {
        public static List<CellGroup> ExecuteStructuredGroups(GameCell[] cells)
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
            return cellGroups;
        }
    }
}