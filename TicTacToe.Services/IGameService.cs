using System;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;
using Player = TicTacToe.Models.MVC.Game.Player;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        void Create(int activePlayerId, int computerPlayerId);
        Game GetCurrentGame(int playerId);
        void Stop(Models.MVC.Game.Game game);
    }

    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
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
    }
}