using GymTron.Domain.Entities;

namespace GymTron.Domain.Repositories;

public interface IExerciseRepository : IEntityRepository<Exercise, int>
{
    Task AddRange(List<Exercise> exercises);
    Task<List<Exercise>> ListAll();
    Task<List<Exercise>> ListByTraining(int trainingId);
}
