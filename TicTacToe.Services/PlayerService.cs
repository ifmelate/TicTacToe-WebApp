using TicTacToe.Models.MVC.Game;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Services
{
    public interface IPlayerService
    {
        Models.MVC.Game.Player Find(string userIp);
        void Create(TicTacToe.Models.MVC.Game.Player player, int currentUserId);
        void Update(Player player);
    }
    public class PlayerService: IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public Models.MVC.Game.Player Find(string userIp)
        {
            var player = _playerRepository.FindByUser(userIp);
            if (player == null)
                return null;
            return new TicTacToe.Models.MVC.Game.Player
            {
                GameSide = (GameSideEnum) player.GameSide.Id,
                Name = player.Name,
                Id = player.Id
            };
        }

        public void Create(TicTacToe.Models.MVC.Game.Player player, int userId)
        {
            _playerRepository.Add(new Models.Entity.Player
            {
                UserId = userId,
                GameSideId = (int) player.GameSide,
                Name = player.Name
            });
            _playerRepository.SaveChanges();
        }

        public void Update(Player newPlayer)
        {
            var player = _playerRepository.GetById(newPlayer.Id);
            player.GameSideId = (int) newPlayer.GameSide;
            player.Name = newPlayer.Name;
            _playerRepository.Update(player);
            _playerRepository.SaveChanges();
        }
    }

   
}