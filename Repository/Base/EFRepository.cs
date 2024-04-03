using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Base
{
    public class EFRepository<T>(DbContext _context, DbSet<T> _dbSet) : IRepository<T> where T : class
    {
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            var entity = _dbSet.Find(id);
            return entity ?? throw new Exception($"Entity with id {id} not found");
        }

        public T GetById(Guid id)
        {
            var entity = _dbSet.Find(id);
            return entity ?? throw new Exception($"Entity with id {id} not found");
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}