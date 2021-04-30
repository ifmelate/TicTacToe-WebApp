using System;
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
using Player = TicTacToe.Models.MVC.Game.Player;

namespace TicTacToe.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IPlayerService _playerService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IGameService _gameService;

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

        public GameController(ILogger<GameController> logger, IPlayerService playerService, IUserService userService,
            IHttpContextAccessor httpContextAccessor, IGameService gameService)
        {
            _logger = logger;
            _playerService = playerService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var player = _playerService.Find(CurrentUser.Ip);
            if (player == null)
                player = new Player();
            var game = new Game
            {
                Player = player,
                StartDateTime = player.Id > 0 ?_gameService.GetCurrentGame(player.Id)?.StartDateTime: null
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
        public IActionResult StartGame(Player player)
        {
            #region Create or Update Player
            var existPlayer = _playerService.Find(CurrentUser.Ip);
            if (existPlayer == null)
            {
                _playerService.Create(player, CurrentUser.Id);
                existPlayer = _playerService.Find(CurrentUser.Ip);
            }
               
            else
                _playerService.Update(player);
            #endregion

            #region Create Game
            var computerPlayer = _playerService.GetComputerPlayer(player.GameSide == GameSideEnum.Crosses? GameSideEnum.Zeros: GameSideEnum.Crosses);

            _gameService.Create(existPlayer.Id, computerPlayer.Id);
            #endregion
            return NoContent();
        }
    }
}
