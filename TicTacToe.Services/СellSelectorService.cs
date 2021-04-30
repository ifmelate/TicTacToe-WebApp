using System;
using System.Linq;
using TicTacToe.Models.Entity;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Services.Strategy;

namespace TicTacToe.Services
{
    public interface IGameCellSelectorService
    {
        GameCell Execute(int currentGameId, GameSideEnum computerGameSide, ISelectorStrategy selectorStrategy);
    }
    /// <summary>
    /// implementing  selection of cells by computer between two strategies
    /// </summary>
    public class GameCellSelectorService: IGameCellSelectorService
    {
        private readonly IGameCellService _gameCellService;

        public GameCellSelectorService(IGameCellService gameCellService)
        {
            _gameCellService = gameCellService;
        }

        public GameCell Execute(int currentGameId, GameSideEnum computerGameSide, ISelectorStrategy selectorStrategy)
        {
            var cells = _gameCellService.GetAll(currentGameId).ToArray();

            GameCell gameCell = selectorStrategy.GetCell(cells, computerGameSide);
 
            return gameCell;
        }

    }
}