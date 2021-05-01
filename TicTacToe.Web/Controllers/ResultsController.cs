using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;

namespace TicTacToe.Web.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IResultsService _resultsService;

        public ResultsController(IResultsService resultsService)
        {
            _resultsService = resultsService;
        }
        public IActionResult Index()
        {
            var results = _resultsService.GetResults();
            return View(results);
        }
    }
}
