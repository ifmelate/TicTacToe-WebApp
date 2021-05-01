using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.MVC.Results;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Services
{
    public interface IResultsService
    {
        Results GetResults();
    }
    public class ResultsService: IResultsService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerService _playerService;

        public ResultsService(IGameRepository gameRepository, IPlayerService playerService)
        {
            _gameRepository = gameRepository;
            _playerService = playerService;
        }

        public Results GetResults()
        {
            var gameResults = _gameRepository.GetAllWithPlayers().Where(s=>s.EndDateTime != null)
                .OrderByDescending(s=>s.StartDateTime)
                .Select(s=> new GameResult
                {
                    GameId = s.Id,
                    Player = _playerService.FindById(s.Player.Id),
                    EndDateTime = s.EndDateTime,
                    StartDateTime = s.StartDateTime,
                    WinnerPlayer = s.WinnerPlayer
                })
                .ToList();
            return new Results {GameResults = gameResults };
        }
    }

   
}
