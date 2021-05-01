using System.Collections;
using System.Collections.Generic;
using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        /// <summary>
        /// Current game by playerId (uncompleted game)
        /// </summary>
        /// <param name="playerId"></param>
        Game GetCurrentByPlayerId(int playerId);

        Game GetWithPlayer(int gameId);
        IEnumerable<Game> GetAllWithPlayers();
    }
}