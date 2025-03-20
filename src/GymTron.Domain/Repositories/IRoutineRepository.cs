using GymTron.Domain.Entities;

namespace GymTron.Domain.Repositories;

public interface IRoutineRepository : IEntityRepository<Routine, int>
{
    Task<List<Routine>> ListAll();
}
