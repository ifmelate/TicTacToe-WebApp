using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.EF;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    public class CellRepository : GenericRepository<Cell>, ICellRepository
    {
        private readonly DbSet<Cell> _dbSet;

        public CellRepository(DataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Cell>();
        }

        public IEnumerable<Cell> GetAllWithMoves()
        {
            return _dbSet.AsNoTracking().Include(d => d.Moves).ThenInclude(s=>s.Player).ToList();
        }
    }
}