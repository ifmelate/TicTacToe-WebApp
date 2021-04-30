using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Services
{
    public interface IGameCellService
    {
        IList<GameCell> GetAll(int gameId);
        IList<GameCell> GetAllAvailable(int gameId);
    }
    public class GameCellService: IGameCellService
    {
        private readonly ICellRepository _cellRepository;

        public GameCellService(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }
        public IList<GameCell> GetAll(int gameId)
        {
            var gameCells = GetList(gameId);

            return gameCells;
        }

        public IList<GameCell> GetAllAvailable(int gameId)
        {
            var gameCells = GetList(gameId);
            return gameCells.Where(d=>d.GameSide == null).ToList();
        }
        private IList<GameCell> GetList(int gameId)
        {
            var cells = _cellRepository.GetAllWithMoves();
            IList<GameCell> gameCells = new List<GameCell>();
            foreach (var cell in cells)
            {
                var gameCell = new GameCell
                {
                    GameId = gameId,
                    CellId = cell.Id,
                    Cell = cell
                };
                var move = cell.Moves.FirstOrDefault(d => d.GameId == gameId && d.CellId == cell.Id);
                if (move != null)
                    gameCell.GameSide = move.Player.GameSideId != null ? (GameSideEnum?)move.Player.GameSideId : null;

                gameCells.Add(gameCell);
            }

            return gameCells;
        }
    }

    
}