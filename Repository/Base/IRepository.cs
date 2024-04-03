using System.Linq.Expressions;

namespace Repository.Base
{
    public interface IRepository<T>
    {
        T GetById(int id);

        T GetById(Guid id);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}