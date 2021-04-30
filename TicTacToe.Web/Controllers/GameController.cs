using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TicTacToe.Models.Entity;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Services;
using TicTacToe.Web.Models;
using Game = TicTacToe.Models.MVC.Game.Game;
using Level = TicTacToe.Models.MVC.Game.Level;
using Player = TicTacToe.Models.MVC.Game.Player;

namespace TicTacToe.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IPlayerService _playerService;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GameService _gameService;
        private readonly IGameCellService _gameCellService;

        public User CurrentUser
        {
            get
            {
                var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
                var user = _userService.FindByIp(remoteIpAddress.ToString());
                if (user == null)
                {
                    _userService.Create(remoteIpAddress.ToString());
                    user = _userService.FindByIp(remoteIpAddress.ToString());
                }

                return user;
            }
        }

        public GameController(ILogger<GameController> logger, IPlayerService playerService, UserService userService,
            IHttpContextAccessor httpContextAccessor, GameService gameService, IGameCellService gameCellService)
        {
            _logger = logger;
            _playerService = playerService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _gameService = gameService;
            _gameCellService = gameCellService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var player = _playerService.Find(CurrentUser.Ip) ?? new Player();

            var currentGame = _gameService.GetCurrentGame(player.Id);
            var game = new Game
            {
                Id = currentGame?.Id ?? 0,
                Player = player,
                StartDateTime = player.Id > 0? currentGame?.StartDateTime : null,
                GameCells = currentGame?.Id > 0 ? CreateGameCellsList(currentGame.Id): new List<GameCell>(),
                Level = new Level() { LevelEnum = LevelEnum.Easy}
            };
            return View(game);
        }

        public IActionResult Results()
        {
            return NoContent();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception

            if (exception != null)
            {
                ;

            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StartGame(Game game)
        {
            #region Create or Update Player
            var existPlayer = _playerService.Find(CurrentUser.Ip);
            if (existPlayer == null)
            {
                _playerService.Create(game.Player, CurrentUser.Id);
                existPlayer = _playerService.Find(CurrentUser.Ip);
            }
               
            else
                _playerService.Update(game.Player);
            #endregion

            #region Create Game
            var computerPlayer = _playerService.GetComputerPlayer(game.Player.GameSide == GameSideEnum.Crosses? GameSideEnum.Zeros: GameSideEnum.Crosses);

           var newGame =_gameService.Create(existPlayer.Id, computerPlayer.Id, game.Level);
            #endregion

            if (game.Player.GameSide == GameSideEnum.Zeros)
                _gameService.MakeComputerMove(newGame.Id);
  

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StopGame(Game game)
        {
            _gameService.Stop(game);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AjaxMakePlayerMove(int gameId, int cellId)
        {
            _gameService.MakePlayerMove(gameId, cellId);
            _gameService.MakeComputerMove(gameId);
            return PartialView("GameGridPartial", CreateGameCellsList(gameId));
        }

        private IList<GameCell> CreateGameCellsList(int gameId)
        {
           return _gameCellService.GetAll(gameId);
        }
    }
}
