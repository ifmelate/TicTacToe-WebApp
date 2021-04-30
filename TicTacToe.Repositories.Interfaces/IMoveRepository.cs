using System.Collections.Generic;
using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IMoveRepository : IGenericRepository<Move>
    {
        int ExistsCount(int gameId, int playerId);
    }
}