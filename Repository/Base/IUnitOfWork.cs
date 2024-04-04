using Domain.Interfaces;

namespace Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class, IEntity;

        Task SaveAsync();

        void RollBack();
    }
}