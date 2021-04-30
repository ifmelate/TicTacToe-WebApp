using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IMoveRepository : IGenericRepository<Move>
    {
        int ExistCount(int gameId, int playerId);
    }
}