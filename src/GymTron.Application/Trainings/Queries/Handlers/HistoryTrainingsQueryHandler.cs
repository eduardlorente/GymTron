using GymTron.Application.Base;
using GymTron.Domain.Aggregates;
using GymTron.Domain.Projections;
using GymTron.Domain.Repositories;
using GymTron.Domain.Services;

namespace GymTron.Application.Trainings.Queries.Handlers;

internal class HistoryTrainingsQueryHandler(ITrainingRepository trainingRepository, IExceptionLogger<HistoryTrainingsQuery> logger)
    : BaseQueryHandler<HistoryTrainingsQuery, List<TrainingHistoryProjection>>(logger)
{


    private readonly ITrainingRepository _trainingRepository = trainingRepository;


    protected override async Task<List<TrainingHistoryProjection>> HandleQuery(HistoryTrainingsQuery request, CancellationToken cancellationToken)
    {
        List<Training> trainings = await _trainingRepository.ListAllWithoutExercises();

        List<Training> completedTrainings = trainings.Where(t => t.Status.IsCompleted).ToList();

        return await Task.FromResult(trainings.Select(t => new TrainingHistoryProjection
        {
            StartedOn = t.StartedOn
        }).ToList());
    }
}
