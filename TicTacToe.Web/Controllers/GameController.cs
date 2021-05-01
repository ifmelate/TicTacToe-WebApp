using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGameService _gameService;
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

        public GameController(ILogger<GameController> logger, IUserService userService,
            IHttpContextAccessor httpContextAccessor, IGameService gameService, IGameCellService gameCellService)
        {
            _logger = logger;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _gameService = gameService;
            _gameCellService = gameCellService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var currentGame = _gameService.GetCurrentGame(CurrentUser.Ip);
            return View(currentGame);
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
                _logger.LogError(exception.Message);
                if (exception.InnerException != null)
                    _logger.LogError(exception.InnerException.Message);

            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StartGame(Game game)
        {
            if (!ModelState.IsValid)
                return View("Index", game);
            var newGame = _gameService.Create(CurrentUser.Id, game.Player, game.Level);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StopGame(Game game)
        {
            _gameService.Stop(game.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AjaxMakePlayerMove(int gameId, int cellId)
        {
            _gameService.MakePlayerMove(gameId, cellId);
            return PartialView("GameGridPartial", CreateGameCellsList(gameId));
        }
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AjaxMakeComputerMove(int gameId)
        {
            _gameService.MakeComputerMove(gameId);
            return PartialView("GameGridPartial", CreateGameCellsList(gameId));
        }
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult UpdateGameInfo(int gameId)
        {
            var currentGame = _gameService.GetGame(gameId);
            return PartialView("GameInfoPartial", currentGame);
        }
        private IList<GameCell> CreateGameCellsList(int gameId)
        {
            return _gameCellService.GetAll(gameId);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
