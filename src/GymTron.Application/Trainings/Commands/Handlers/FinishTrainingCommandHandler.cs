using FluentValidation;
using GymTron.Application.Base;
using GymTron.Domain.Repositories;
using GymTron.Domain.Services;
using MediatR;

namespace GymTron.Application.Trainings.Commands.Handlers;

internal class FinishTrainingCommandHandler(IMediator mediator,
                                            ITrainingRepository trainingRepository,
                                            IExerciseRepository exerciseRepository,
                                            IExceptionLogger<FinishTrainingCommand> logger)
    : BaseCommandHandler<FinishTrainingCommand>(logger)
{


    private readonly IMediator _mediator = mediator;
    private readonly ITrainingRepository _trainingRepository = trainingRepository;
    private readonly IExerciseRepository _exerciseRepository = exerciseRepository;


    protected override async Task HandleCommand(FinishTrainingCommand request, CancellationToken cancellationToken)
    {
        FinishTrainingCommandValidator validator = new();
        validator.ValidateAndThrow(request);

        request.Training.Complete();

        await _trainingRepository.Update(request.Training);

        await _exerciseRepository.AddRange(request.Training.CompletedWorkout);

        await request.Training.RaiseEvents(_mediator);
    }
}
