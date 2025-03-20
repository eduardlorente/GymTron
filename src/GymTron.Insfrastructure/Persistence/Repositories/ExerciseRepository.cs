using GymTron.Domain.Entities;
using GymTron.Domain.Repositories;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL;

namespace GymTron.Insfrastructure.Persistence.Repositories;

internal class ExerciseRepository(IExerciseDAL exerciseDAL) : IExerciseRepository
{


    private readonly IExerciseDAL _exerciseDAL = exerciseDAL;


    public async Task Add(Exercise entity)
    {
        await _exerciseDAL.Add(entity);
    }


    public async Task<Exercise?> GetById(int id)
    {
        throw new NotImplementedException();
    }


    public async Task AddRange(List<Exercise> exercises)
    {
        await _exerciseDAL.AddRange(exercises);
    }


    public async Task<List<Exercise>> ListByTraining(int trainingId)
    {
        IEnumerable<ExerciseDALModel> exercises = await _exerciseDAL.ListByTrainingId(trainingId);

        return BuildExercisesList(exercises);
    }


    public async Task Update(Exercise entity)
    {
        throw new NotImplementedException();
    }


    public async Task<List<Exercise>> ListAll()
    {
        IEnumerable<ExerciseDALModel> exercises = await _exerciseDAL.ListAll();

        return BuildExercisesList(exercises);
    }


    private static List<Exercise> BuildExercisesList(IEnumerable<ExerciseDALModel> exercises)
    {
        return exercises.Select(e => Exercise.FromDatabase(e.Id,
                                                           e.TrainingId,
                                                           e.ExerciseParametersId,
                                                           e.Name,
                                                           e.Weight,
                                                           e.DurationInSeconds,
                                                           e.Repetitions,
                                                           e.CreatedOn,
                                                           [.. e.ObservationsCSV.Split(';')]))
                        .ToList();
    }
}
