using FluentValidation;
using GymTron.Application.Base;

namespace GymTron.Application.BodyWeights.Commands;

public class RegisterBodyWeightCommand(Guid correlationId, decimal weight, decimal imc)
    : CommandBase(correlationId)
{


    public decimal Weight { get; private set; } = weight;
    public decimal IMC { get; private set; } = imc;
}


public class RegisterBodyWeightCommandValidator : AbstractValidator<RegisterBodyWeightCommand>
{
    public RegisterBodyWeightCommandValidator()
    {
        RuleFor(x => x.Weight).NotEmpty().GreaterThan(0);
        RuleFor(x => x.IMC).NotEmpty().GreaterThan(0);
    }
}
