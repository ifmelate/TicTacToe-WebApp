using TicTacToe.Models.Entity;
using TicTacToe.Repositories.GenericRepository;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
    }
}