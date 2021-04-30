using TicTacToe.Models.MVC.Game;

namespace TicTacToe.Services.Strategy
{
    internal interface ISelectorStrategy
    {
        GameCell GetCell(GameCell[] cells);
    }
}