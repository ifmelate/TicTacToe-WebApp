using System;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Services
{
    public interface IUserService
    {
        User FindByIp(string userIp);
        void Create(string ip);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User FindByIp(string userIp)
        {
            return _userRepository.FindUser(userIp);
        }

        public void Create(string ip)
        {
            _userRepository.Add(new User
            {
                Ip =  ip,
            });
            _userRepository.SaveChanges();
        }
    }
}