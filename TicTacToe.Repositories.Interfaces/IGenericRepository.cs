using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace TicTacToe.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(ICollection<T> entities);
        int Count(Expression<Func<T, bool>> match);
        Task<int> CountAsync(Expression<Func<T, bool>> match);

        T Find(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        ICollection<T> FindAll(Expression<Func<T, bool>> match,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        bool Any(Expression<Func<T, bool>> match);
        Task<bool> AnyAsync(Expression<Func<T, bool>> match);
    }
}