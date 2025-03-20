using GymTron.Domain.Entities;

namespace GymTron.Domain.Repositories;

public interface IEntityRepository<T, TId> : IRepository<T>
    where T : Entity<TId>
{
    Task<T?> GetById(TId id);
    Task Add(T entity);
    Task Update(T entity);
}
