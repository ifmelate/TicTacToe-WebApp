using System;
using TicTacToe.Models.Entity;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Repositories.Interfaces;
using TicTacToe.Services.Strategy;
using Game = TicTacToe.Models.Entity.Game;
using Level = TicTacToe.Models.MVC.Game.Level;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        Game Create(int activePlayerId, int computerPlayerId, Level level);
        Game GetCurrentGame(int playerId);
        void Stop(Models.MVC.Game.Game game);

        void MakePlayerMove(int gameId, int cellId);

        void MakeComputerMove(int gameId);
    }

    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMoveRepository _moveRepository;
        private readonly IGameCellSelectorService _gameCellSelectorService;

        public GameService(IGameRepository gameRepository, IMoveRepository moveRepository,
            IGameCellSelectorService gameCellSelectorService)
        {
            _gameRepository = gameRepository;
            _moveRepository = moveRepository;
            _gameCellSelectorService = gameCellSelectorService;
        }

        public Game Create(int activePlayerId, int computerPlayerId, Level gameLevel)
        {
            _gameRepository.Add(new Game
            {
                PlayerId = activePlayerId,
                ComputerPlayerId = computerPlayerId,
                LevelId = (int)gameLevel.LevelEnum
            });
            _gameRepository.SaveChanges();
            return GetCurrentGame(activePlayerId);
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

        public void MakePlayerMove(int gameId, int cellId)
        {
            var currentGame = _gameRepository.GetWithPlayer(gameId);
            #region Player Move

            _moveRepository.Add(new Move
            {
                CellId = cellId,
                GameId = gameId,
                PlayerId = currentGame.PlayerId,
                MoveNumber = _moveRepository.ExistsCount(gameId, currentGame.Player.Id) + 1
            });
            _moveRepository.SaveChanges();

            #endregion
        }

        public void MakeComputerMove(int gameId)
        {
            var currentGame = _gameRepository.GetWithPlayer(gameId);
            ISelectorStrategy strategy = currentGame.LevelId == (int) LevelEnum.Easy ?
                (ISelectorStrategy) new EasySelectorStrategy()
                : (ISelectorStrategy) new HardSelectorStrategy();
            
            var cell = _gameCellSelectorService.Execute(currentGame.Id, (GameSideEnum)currentGame.ComputerPlayer.GameSideId , strategy);

            #region Computer move

            _moveRepository.Add(new Move
            {
                CellId = cell.CellId,
                GameId = gameId,
                PlayerId = currentGame.ComputerPlayerId,
                MoveNumber = _moveRepository.ExistsCount(gameId, currentGame.ComputerPlayerId) + 1
            });
            _moveRepository.SaveChanges();
            #endregion

        }
    }
}