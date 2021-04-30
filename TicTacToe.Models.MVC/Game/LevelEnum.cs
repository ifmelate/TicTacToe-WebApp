using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models.MVC.Game
{
    public enum LevelEnum
    {
        [Display(Name = "EasyLevel", ResourceType = typeof(TicTacToe.Resources.GameStrings))]
        Easy = 1,
        [Display(Name = "HardLevel", ResourceType = typeof(TicTacToe.Resources.GameStrings))]
        Hard = 2
    }
}