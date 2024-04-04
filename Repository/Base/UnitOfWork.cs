using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository.Base
{
    public class UnitOfWork(EShopContext _context) : IUnitOfWork
    {
        private Dictionary<string, object> _repositories = [];

        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new EFRepository<T>(_context);
                _repositories.Add(type, repoInstance);
            }
            return (IRepository<T>)_repositories[type];
        }

        public void Dispose()
        {
            _context.Dispose();
            _repositories.Clear();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void RollBack()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}