using FluentValidation;
using GymTron.Application.Base;
using GymTron.Domain.Aggregates;
using GymTron.Domain.Entities;
using GymTron.Domain.Exceptions;
using GymTron.Domain.Repositories;
using GymTron.Domain.Services;
using MediatR;

namespace GymTron.Application.Trainings.Commands.Handlers;

internal class StartTrainingCommandHandler(IMediator mediator,
                                           ITrainingRepository trainingRepository,
                                           IRoutineRepository routineRepository,
                                           IExceptionLogger<StartTrainingCommand> logger)
    : BaseCommandHandler<StartTrainingCommand>(logger)
{


    private readonly IMediator _mediator = mediator;
    private readonly ITrainingRepository _trainingRepository = trainingRepository;
    private readonly IRoutineRepository _routineRepository = routineRepository;


    protected override async Task HandleCommand(StartTrainingCommand request, CancellationToken cancellationToken)
    {
        StartTrainingCommandValidator validator = new();
        validator.ValidateAndThrow(request);

        await AvoidStartTrainingIfExistsACurrentOne();

        Routine routine = await _routineRepository.GetById(request.RoutineId) ?? throw new EntityNotFoundException(nameof(Routine));

        Training training = Training.CreateAnStartedTraining(request.RoutineId,
                                                             request.DayOfWeek,
                                                             routine.WorkByDays[request.DayOfWeek]);

        await _trainingRepository.Add(training);

        await training.RaiseEvents(_mediator);
    }


    private async Task AvoidStartTrainingIfExistsACurrentOne()
    {
        Training? lastUserTraining = await _trainingRepository.GetCurrent();

        if (lastUserTraining != null)
            throw new Exception("The previous training is not ended.");
    }
}
