using System;
using System.Collections.Generic;
using TicTacToe.Models.Entity;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Repositories.Interfaces;
using TicTacToe.Services.Strategy;
using Game = TicTacToe.Models.Entity.Game;
using Level = TicTacToe.Models.MVC.Game.Level;
using Player = TicTacToe.Models.Entity.Player;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        Models.MVC.Game.Game Create(int userId, Models.MVC.Game.Player player, Level gameLevel);
        TicTacToe.Models.MVC.Game.Game GetCurrentGame(string userIp);
        void Stop(Models.MVC.Game.Game game);

        void MakePlayerMove(int gameId, int cellId);

        void MakeComputerMove(int gameId);
    }

    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMoveRepository _moveRepository;
        private readonly IGameCellSelectorService _gameCellSelectorService;
        private readonly IVictoryCheckService _victoryCheckService;
        private readonly IPlayerService _playerService;
        private readonly IGameCellService _gameCellService;

        public GameService(IGameRepository gameRepository, IMoveRepository moveRepository,
            IGameCellSelectorService gameCellSelectorService, IVictoryCheckService victoryCheckService,
            IPlayerService playerService, IGameCellService gameCellService)
        {
            _gameRepository = gameRepository;
            _moveRepository = moveRepository;
            _gameCellSelectorService = gameCellSelectorService;
            _victoryCheckService = victoryCheckService;
            _playerService = playerService;
            _gameCellService = gameCellService;
        }

        public Models.MVC.Game.Game Create(int userId, Models.MVC.Game.Player player, Level gameLevel)
        {
            #region Create or Update Player
            var existPlayer = _playerService.FindById(player.Id);
            if (existPlayer == null)
            {
                existPlayer = _playerService.Create(player, userId);
    
            }
            else
                _playerService.Update(player);
            #endregion

            
            var computerPlayer = _playerService.GetComputerPlayer(player.GameSide == GameSideEnum.Crosses ? GameSideEnum.Zeros : GameSideEnum.Crosses);


            _gameRepository.Add(new Game
            {
                PlayerId = existPlayer.Id,
                ComputerPlayerId = computerPlayer.Id,
                LevelId = (int)gameLevel.LevelEnum
            });
            _gameRepository.SaveChanges(); 

            var currentGame = CurrentGame(existPlayer);
            if (player.GameSide == GameSideEnum.Zeros)
                MakeComputerMove(currentGame.Id);
            return currentGame;
        }

        public TicTacToe.Models.MVC.Game.Game GetCurrentGame(string userIp)
        {
            var player = _playerService.FindByUserIp(userIp) ?? new TicTacToe.Models.MVC.Game.Player();
            return CurrentGame(player);
        }

        private Models.MVC.Game.Game CurrentGame(TicTacToe.Models.MVC.Game.Player player)
        {
            var currentGame = _gameRepository.GetCurrentByPlayerId(player.Id);
            var game = new TicTacToe.Models.MVC.Game.Game()
            {
                Id = currentGame?.Id ?? 0,
                Player = player,
                StartDateTime = player.Id > 0 ? currentGame?.StartDateTime : null,
                GameCells = currentGame?.Id > 0 ? _gameCellService.GetAll(currentGame.Id) : new List<GameCell>(),
                Level = new Level() {LevelEnum = LevelEnum.Easy}
            };
            return game;
        }

        public void Stop(Models.MVC.Game.Game game)
        {
            StopGame(game.Player.Id);
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
            CheckWin(currentGame);

            #endregion
        }


        public void MakeComputerMove(int gameId)
        {
            var currentGame = _gameRepository.GetWithPlayer(gameId);
            ISelectorStrategy strategy = currentGame.LevelId == (int)LevelEnum.Easy ?
                new EasySelectorStrategy()
                : (ISelectorStrategy)new HardSelectorStrategy();

            var cell = _gameCellSelectorService.Execute(currentGame.Id, (GameSideEnum)currentGame.ComputerPlayer.GameSideId, strategy);

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
        private void StopGame(int playerId)
        {
            var currentGame = _gameRepository.GetCurrentByPlayerId(playerId);
            currentGame.EndDateTime = DateTime.Now;
            _gameRepository.Update(currentGame);
            _gameRepository.SaveChanges();
        }
        private void EndGame(Game game, GameSideEnum winnerGameSideEnum)
        {
            var currentGame = _gameRepository.GetCurrentByPlayerId(game.PlayerId);
            currentGame.EndDateTime = DateTime.Now;
            currentGame.WinnerPlayerId = game.Player.GameSideId == (int)winnerGameSideEnum ?
                game.PlayerId :
                game.ComputerPlayerId;
            currentGame.LoserPlayerId = game.Player.GameSideId != (int)winnerGameSideEnum ?
                game.PlayerId :
                game.ComputerPlayerId;
            _gameRepository.Update(currentGame);
            _gameRepository.SaveChanges();

        }
        private void CheckWin(Game currentGame)
        {
            var winner = _victoryCheckService.GetWinner(currentGame);
            if (winner != null)
            {
                EndGame(currentGame, (GameSideEnum) winner);
            }
        }
    }
}