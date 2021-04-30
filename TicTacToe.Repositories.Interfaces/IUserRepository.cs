using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindUser(string ip);
    }
}