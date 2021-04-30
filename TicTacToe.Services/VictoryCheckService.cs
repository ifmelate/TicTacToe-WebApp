using System.Linq;
using TicTacToe.Models.MVC.Game;
using Game = TicTacToe.Models.Entity.Game;

namespace TicTacToe.Services
{
    public interface IVictoryCheckService
    {
        GameSideEnum? GetWinner(Game currentGame);
    }
    public class VictoryCheckService: IVictoryCheckService
    {
        private readonly IGameCellService _gameCellService;

        public VictoryCheckService(IGameCellService gameCellService)
        {
            _gameCellService = gameCellService;
        }
        public GameSideEnum? GetWinner(Game currentGame)
        {
            var cells = _gameCellService.GetAll(currentGame.Id).ToArray();
            var cellGroups = CellGroupsHelper.ExecuteStructuredGroups(cells);

           var crossesWinnerGroup = cellGroups.FirstOrDefault(d => d.OccupiedGameCells.Count(s => s.GameSide == GameSideEnum.Crosses) == 3);
           if (crossesWinnerGroup != null)
               return GameSideEnum.Crosses;
           var zerosWinnerGroup = cellGroups.FirstOrDefault(d => d.OccupiedGameCells.Count(s => s.GameSide == GameSideEnum.Zeros) == 3);
           if (zerosWinnerGroup != null)
               return GameSideEnum.Zeros;

           return null;
        }
    }
}