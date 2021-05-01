using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models.MVC.Game
{
    public class Level
    {
        [Required]
        public LevelEnum LevelEnum { get; set; }
    }
}