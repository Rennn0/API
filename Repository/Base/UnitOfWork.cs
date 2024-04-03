using Microsoft.EntityFrameworkCore;

namespace Repository.Base
{
    public class UnitOfWork(EShopContext _context) : IUnitOfWork
    {
        private Dictionary<string, object> _repositories = [];

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new EFRepository<T>(_context, _context.Set<T>());
                _repositories.Add(type, repoInstance);
            }
            return (IRepository<T>)_repositories[type];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            _repositories.Clear();
            GC.SuppressFinalize(this);
        }
    }
}