using System.ComponentModel.DataAnnotations;
using TicTacToe.Models.Entity;

namespace TicTacToe.Models.MVC.Game
{
    public class Player
    {
        public int Id { get; set; }
        public Player()
        {
            GameSideEnum = GameSideEnum.Crosses;
        }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public  GameSideEnum GameSideEnum  { get; set; }

    }
}