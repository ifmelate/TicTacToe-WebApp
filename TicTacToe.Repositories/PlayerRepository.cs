using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.EF;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        private readonly DbSet<Player> _dbSet;

        public PlayerRepository(DataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Player>();
        }


        public Player FindByUser(string ip)
        {
            return _dbSet.Include(d=>d.GameSide)
                .FirstOrDefault(d => d.User.Ip == ip);
        }
    }
}