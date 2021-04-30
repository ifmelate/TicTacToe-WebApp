using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.EF;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private readonly DbSet<Game> _dbSet;

        public GameRepository(DataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Game>();
        }

        /// <summary>
        /// Current game by playerId (uncompleted game)
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public Game GetCurrentByPlayerId(int playerId)
        {
            return _dbSet.OrderBy(s=>s.StartDateTime).LastOrDefault(d => d.PlayerId == playerId && d.EndDateTime == null);
        }

        public Game GetWithPlayer(int gameId)
        {
            return _dbSet.Include(d => d.Player).FirstOrDefault(d => d.Id == gameId);
        }
    }
}