using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.EF;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    public class MoveRepository : GenericRepository<Move>, IMoveRepository
    {
        private readonly DbSet<Move> _dbSet;

        public MoveRepository(DataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Move>();
        }

        public int ExistCount(int gameId, int playerId)
        {
            return _dbSet.Count(d => d.GameId == gameId && d.PlayerId == playerId);
        }
    }
}