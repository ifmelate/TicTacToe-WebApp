using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TicTacToe.Data.EF;
using TicTacToe.Repositories.GenericRepository;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    public  class GenericRepository<T> :
      IGenericRepository<T>
         where T : class
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public void Delete(ICollection<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public int Count(Expression<Func<T, bool>> match)
        {
            return _dbSet.Count(match);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> match)
        {
            return await _dbSet.CountAsync(match);
        }

        public T Find(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            return Filter(match,orderBy,includes).FirstOrDefault();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                query = includes(query);
            }
            if (match != null)
            {
                query = query.Where(match);
            }

           
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            return Filter(match, orderBy, includes).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            return await Filter(match, orderBy, includes).ToListAsync();
        }

        public bool Any(Expression<Func<T, bool>> match)
        {
            return _dbSet.Any(match);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> match)
        {
            return await _dbSet.AnyAsync(match);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        private IQueryable<T> Filter(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                query = includes(query);

            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }


    }
}
