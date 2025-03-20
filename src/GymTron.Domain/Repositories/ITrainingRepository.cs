using GymTron.Domain.Aggregates;

namespace GymTron.Domain.Repositories;

public interface ITrainingRepository : IEntityRepository<Training, int>
{
    Task<Training?> GetCurrent();
    Task<List<Training>> ListAllWithoutExercises();
}
