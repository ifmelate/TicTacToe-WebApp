using TicTacToe.Models.MVC.Game;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Services
{
    public interface IPlayerService
    {
        Models.MVC.Game.Player FindByUserIp(string userIp);
        Models.MVC.Game.Player FindById(int playerId);
        Player Create(TicTacToe.Models.MVC.Game.Player player, int currentUserId);
        void Update(Player player);
        Player GetComputerPlayer(GameSideEnum gameSide);
    }
    public class PlayerService: IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public Models.MVC.Game.Player FindByUserIp(string userIp)
        {
            var player = _playerRepository.FindByUser(userIp);
            if (player == null)
                return null;
            return ConvertToMVCPlayer(player);
        }
        public Models.MVC.Game.Player FindByUserId(int userId)
        {
            var player = _playerRepository.FindByUserId(userId);
            if (player == null)
                return null;
            return ConvertToMVCPlayer(player);
        }
        public Models.MVC.Game.Player FindById(int playerId)
        {
            var player = _playerRepository.GetWithGameSide(playerId);
            if (player == null)
                return null;
            return ConvertToMVCPlayer(player);
        }

        private Player ConvertToMVCPlayer(Models.Entity.Player player)
        {
            return new TicTacToe.Models.MVC.Game.Player
            {
                GameSide = (GameSideEnum) player.GameSide.Id,
                Name = player.Name,
                Id = player.Id
            };
        }

        public TicTacToe.Models.MVC.Game.Player Create(TicTacToe.Models.MVC.Game.Player player, int userId)
        {
            _playerRepository.Add(new Models.Entity.Player
            {
                UserId = userId,
                GameSideId = (int) player.GameSide,
                Name = player.Name
            });
            _playerRepository.SaveChanges();
            return FindByUserId(userId);
        }

        public void Update(Player newPlayer)
        {
            var player = _playerRepository.GetById(newPlayer.Id);
            player.GameSideId = (int) newPlayer.GameSide;
            player.Name = newPlayer.Name;
            _playerRepository.Update(player);
            _playerRepository.SaveChanges();
        }

        public Player GetComputerPlayer(GameSideEnum gameSide)
        {
            var player = _playerRepository.GetComputerPlayer((int) gameSide);
            return ConvertToMVCPlayer(player);
        }
    }

   
}