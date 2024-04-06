using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        DbSet<T> QueryBuilder();

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);

        Task<IEnumerable<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int size, int page, params Expression<Func<T, object>>[] include);

        Task SaveAsync();
    }
}