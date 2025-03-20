using GymTron.Domain.Entities;
using GymTron.Insfrastructure.Persistence.DAL.Models;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal interface IExerciseDAL
{
    Task Add(Exercise entity);
    Task AddRange(List<Exercise> exercises);
    Task<IEnumerable<ExerciseDALModel>> ListByTrainingId(int trainingId);
    Task<IEnumerable<ExerciseDALModel>> ListAll();
}