using TicTacToe.Models.MVC.Game;

namespace TicTacToe.Services.Strategy
{
    public interface ISelectorStrategy
    {
        GameCell GetCell(GameCell[] cells, GameSideEnum computerGameSide);
    }
}