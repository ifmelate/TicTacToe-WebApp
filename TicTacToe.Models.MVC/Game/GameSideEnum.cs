using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models.MVC.Game
{
    public enum GameSideEnum
    {
        [Display(Name = "Crosses", ResourceType = typeof(TicTacToe.Resources.GameStrings))]
        Crosses = 1,
        [Display(Name = "Zeros", ResourceType = typeof(TicTacToe.Resources.GameStrings))]
        Zeros = 2,
    }
}