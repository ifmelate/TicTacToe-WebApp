using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Data.EF;

namespace TicTacToe.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dbContext;


        public IRuKeywordRepository RuKeywords { get; }
      

        public IEnumerable<TEntity> GetFromProcedure<TEntity>(string sql, params object[] sqlParameters) where TEntity : class
        {
            return dbContext.FromSqlRaw<TEntity>(sql, sqlParameters);
        }

        public void ExecuteProcedure<TEntity>(string sql, params object[] sqlParameters) where TEntity : class
        {
            dbContext.ExecuteSqlRaw<TEntity>(sql, sqlParameters);
        }




        public UnitOfWork(DataContext dbContext)
        {
            this.dbContext = dbContext;
            RuKeywords = new RuKeywordRepository(dbContext);
            
        }



        public void Commit()
        {
            dbContext.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        public void Rollback()
        {
            dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }

}

