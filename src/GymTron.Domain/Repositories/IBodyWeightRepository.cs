using GymTron.Domain.Entities;

namespace GymTron.Domain.Repositories;

public interface IBodyWeightRepository : IEntityRepository<BodyWeight, int>
{
    Task<List<BodyWeight>> ListAll();
}
