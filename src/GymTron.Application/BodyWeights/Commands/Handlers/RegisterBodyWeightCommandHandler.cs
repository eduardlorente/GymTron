using FluentValidation;
using GymTron.Application.Base;
using GymTron.Domain.Entities;
using GymTron.Domain.Repositories;
using GymTron.Domain.Services;

namespace GymTron.Application.BodyWeights.Commands.Handlers;

internal class RegisterBodyWeightCommandHandler(IBodyWeightRepository bodyWeightRepository,
                                                IExceptionLogger<RegisterBodyWeightCommand> logger)
    : BaseCommandHandler<RegisterBodyWeightCommand>(logger)
{


    private readonly IBodyWeightRepository _bodyWeightRepository = bodyWeightRepository;


    protected override async Task HandleCommand(RegisterBodyWeightCommand request, CancellationToken cancellationToken)
    {
        RegisterBodyWeightCommandValidator validator = new();
        validator.ValidateAndThrow(request);

        BodyWeight bodyWeight = BodyWeight.New(request.Weight, request.IMC, DateTime.Now);

        await _bodyWeightRepository.Add(bodyWeight);
    }
}
