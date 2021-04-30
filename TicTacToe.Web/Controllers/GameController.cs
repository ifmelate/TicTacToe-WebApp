using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TicTacToe.Models.MVC.Game;
using TicTacToe.Services;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IPlayerService _playerService;

        public GameController(ILogger<GameController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        public IActionResult Index()
        {
            var player = _playerService.Find("");
            if(player == null)
                player = new Player();;
            var game = new Game
            {
                Player = player
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CreatePlayer(Player player)
        {
            _playerService.Create(player);
            return NoContent();
        }
    }
}
