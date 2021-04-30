using System;
using System.Linq;
using TicTacToe.Models.Entity;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Services.Strategy;

namespace TicTacToe.Services
{
    public interface IGameCellSelectorService
    {
        GameCell Execute(int currentGameId, int computerPlayerId, ISelectorStrategy selectorStrategy);
    }
    /// <summary>
    /// implementing  selection of cells by computer between two strategies
    /// </summary>
    public class GameGameCellSelectorService: IGameCellSelectorService
    {
        private readonly IGameCellService _gameCellService;

        public GameGameCellSelectorService(IGameCellService gameCellService)
        {
            _gameCellService = gameCellService;
        }

        public GameCell Execute(int currentGameId, int computerPlayerId, ISelectorStrategy selectorStrategy)
        {
            var cells = _gameCellService.GetAll(currentGameId).ToArray();

            GameCell gameCell = selectorStrategy.GetCell(cells);
 
            return gameCell;
        }

    }
}