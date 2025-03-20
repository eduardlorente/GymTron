using GymTron.Domain.Aggregates;
using GymTron.Domain.Entities;
using GymTron.Domain.Repositories;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL;

namespace GymTron.Insfrastructure.Persistence.Repositories;

internal class TrainingRepository(ITrainingDAL trainingDAL,
                                  IRoutineRepository routineRepository,
                                  IExerciseRepository exerciseRepository) : ITrainingRepository
{


    private readonly ITrainingDAL _trainingDAL = trainingDAL;
    private readonly IRoutineRepository _routineRepository = routineRepository;
    private readonly IExerciseRepository _exerciseRepository = exerciseRepository;


    public async Task Add(Training entity)
    {
        await _trainingDAL.Add(entity);
    }


    public async Task<Training?> GetById(int id)
    {
        TrainingDALModel? trainingData = await _trainingDAL.GetById(id);

        return await BuildTrainingFromData(trainingData);
    }


    public async Task<Training?> GetCurrent()
    {
        TrainingDALModel? trainingData = await _trainingDAL.GetCurrent();

        return await BuildTrainingFromData(trainingData);
    }


    public async Task Update(Training entity)
    {
        await _trainingDAL.Update(new TrainingDALModel(entity));
    }


    public async Task<List<Training>> ListAllWithoutExercises()
    {
        List<TrainingDALModel> trainings = await _trainingDAL.ListAll();

        return trainings.Select(t => Training.FromDatabase(t.Id,
                                                          t.RoutineId,
                                                          t.DayOfWeek,
                                                          t.StartedOn,
                                                          t.CompletedOn,
                                                          (Domain.Enums.EntityStatusTypes)t.StatusType,
                                                          [],
                                                          [])).ToList();
    }


    private async Task<Training?> BuildTrainingFromData(TrainingDALModel? trainingData)
    {
        if (trainingData != null)
        {
            Routine? routine = await _routineRepository.GetById(trainingData.RoutineId);

            if (routine != null)
            {
                List<Exercise> completedExercises = await _exerciseRepository.ListByTraining(trainingData.Id);

                return Training.FromDatabase(trainingData.Id,
                                             trainingData.RoutineId,
                                             trainingData.DayOfWeek,
                                             trainingData.StartedOn,
                                             trainingData.CompletedOn,
                                             (Domain.Enums.EntityStatusTypes)trainingData.StatusType,
                                             routine.WorkByDays[trainingData.DayOfWeek],
                                             completedExercises);
            }
        }

        return null;
    }
}
