using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.EF;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;

        public UserRepository(DataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<User>();
        }
        public User FindUser(string ip)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(d => d.Ip == ip);
        }
    }
}