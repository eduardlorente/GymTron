using GymTron.Application.Base;
using GymTron.Domain.Aggregates;
using GymTron.Domain.Repositories;
using GymTron.Domain.Services;

namespace GymTron.Application.Trainings.Queries.Handlers;

internal class CurrentTrainingQueryHandler(ITrainingRepository trainingRepository, IExceptionLogger<CurrentTrainingQuery> logger)
    : BaseQueryHandler<CurrentTrainingQuery, Training>(logger)
{


    private readonly ITrainingRepository _trainingRepository = trainingRepository;


    protected override async Task<Training> HandleQuery(CurrentTrainingQuery request, CancellationToken cancellationToken)
        => await _trainingRepository.GetCurrent()!;
}
