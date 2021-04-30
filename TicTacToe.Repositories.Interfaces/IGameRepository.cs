using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Game GetByPlayerId(int playerId);
    }
}