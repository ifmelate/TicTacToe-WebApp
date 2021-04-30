using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Game GetCurrentByPlayerId(int playerId);
    }
}