using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.Entity;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Repositories.Interfaces;
using Game = TicTacToe.Models.Entity.Game;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        void Create(int activePlayerId, int computerPlayerId);
        Game GetCurrentGame(int playerId);
        void Stop(Models.MVC.Game.Game game);

        void ExecuteMove(int gameId, int cellId);
    }

    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMoveRepository _moveRepository;
        private readonly ICellRepository _cellRepository;

        public GameService(IGameRepository gameRepository, IMoveRepository moveRepository, ICellRepository cellRepository)
        {
            _gameRepository = gameRepository;
            _moveRepository = moveRepository;
            _cellRepository = cellRepository;
        }

        public void Create(int activePlayerId, int computerPlayerId)
        {
            _gameRepository.Add(new Game
            {
                PlayerId = activePlayerId,
                ComputerPlayerId = computerPlayerId
            });
            _gameRepository.SaveChanges();
        }

        public Game GetCurrentGame(int playerId)
        {
            return _gameRepository.GetCurrentByPlayerId(playerId);
        }

        public void Stop(Models.MVC.Game.Game game)
        {
            var currentGame = _gameRepository.GetCurrentByPlayerId(game.Player.Id);
            currentGame.EndDateTime = DateTime.Now;
            _gameRepository.Update(currentGame);
            _gameRepository.SaveChanges();
        }

        public void ExecuteMove(int gameId, int cellId)
        {
            var currentGame = _gameRepository.GetWithPlayer(gameId);
            _moveRepository.Add(new Move
            {
                CellId = cellId,
                GameId = gameId,
                PlayerId = currentGame.Player.Id,
                MoveNumber = _moveRepository.ExistCount(gameId, currentGame.Player.Id)
            });
            _moveRepository.SaveChanges();
        }

        public IList<GameCell> GetGameCells(int gameId)
        {
            var cells = _cellRepository.GetAllWithMoves();
            IList<GameCell> gameCells = new List<GameCell>();
            foreach (var cell in cells)
            {
                var gameCell = new GameCell
                {
                    GameId = gameId,
                    CellId = cell.Id,
                };
                var move = cell.Moves.FirstOrDefault(d => d.GameId == gameId && d.CellId == cell.Id);
                if (move != null)
                    gameCell.GameSide = move.Player.GameSideId != null? (GameSideEnum?) move.Player.GameSideId: null;

                gameCells.Add(gameCell);
            }

            return gameCells;
        }
    }
}