using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using System.Linq.Expressions;

namespace Repository.Base
{
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly EShopContext _context;
        private readonly DbSet<T> _dbSet;

        public EFRepository(EShopContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int size, int page, params Expression<Func<T, object>>[] include)
        {
            var query = include.Length > 0 ? Includes(include) : _dbSet;
            return await query.Where(predicate).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllPagedAsync(int size, int page, params Expression<Func<T, object>>[] include)
        {
            var query = include.Length > 0 ? Includes(include) : _dbSet;
            return await query.Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include)
        {
            var query = include.Length > 0 ? Includes(include) : _dbSet;
            var result = await query.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
        {
            var query = include.Length > 0 ? Includes(include) : _dbSet;
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            var query = include.Length > 0 ? Includes(include) : _dbSet;
            var entity = await query.SingleOrDefaultAsync(e => e.Id == id);
            return entity ?? throw new NotFoundExc(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> Includes(Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _dbSet;
            foreach (var item in include)
            {
                query = query.Include(item);
            }
            return query;
        }
    }
}