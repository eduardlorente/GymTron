using FluentValidation;
using GymTron.Application.Base;

namespace GymTron.Application.Trainings.Commands;

public class StartTrainingCommand(Guid correlationId, int routineId, int dayOfWeek)
    : CommandBase(correlationId)
{


    public int RoutineId { get; private set; } = routineId;
    public int DayOfWeek { get; private set; } = dayOfWeek;
}


public class StartTrainingCommandValidator : AbstractValidator<StartTrainingCommand>
{
    public StartTrainingCommandValidator()
    {
        RuleFor(x => x.RoutineId).NotEmpty().GreaterThan(0);
    }
}
