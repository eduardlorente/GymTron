using FluentValidation;
using GymTron.Application.Base;
using GymTron.Domain.Aggregates;
using GymTron.Domain.Entities;
using GymTron.Domain.Services;

namespace GymTron.Application.Trainings.Commands.Handlers;

internal class AddExerciseToTrainingCommandHandler(IExceptionLogger<AddExerciseToTrainingCommand> logger)
    : BaseCommandHandlerWithResponse<AddExerciseToTrainingCommand, Training>(logger)
{


    protected override async Task<Training> HandleCommand(AddExerciseToTrainingCommand request, CancellationToken cancellationToken)
    {
        AddExerciseToTrainingCommandValidator validator = new();
        validator.ValidateAndThrow(request);



        Exercise exercise = Exercise.New(request.CurrentTraining!.Id!,
                                         request.ExerciseParametersId,
                                         request.ExerciseParametersName,
                                         request.Weight ?? 0,
                                         request.DurationInSeconds ?? 0,
                                         request.Repetitions ?? 0,
                                         request.Observations ?? []);

        request.CurrentTraining.CompleteExercise(exercise);

        return request.CurrentTraining;
    }
}
