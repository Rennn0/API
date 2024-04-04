using System.Linq.Expressions;

namespace Repository.Base
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);

        Task<IEnumerable<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int size, int page, params Expression<Func<T, object>>[] include);

        Task SaveAsync();
    }
}