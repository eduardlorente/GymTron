using FluentValidation;
using GymTron.Domain.Entities;
using GymTron.Domain.Repositories;
using MediatR;

namespace GymTron.Application.Logs.Commands.Handlers;

internal class RegisterLogCommandHandler(ILogRepository logRepository)
    : IRequestHandler<RegisterLogCommand>
{


    private readonly ILogRepository _logRepository = logRepository;


    public async Task Handle(RegisterLogCommand request, CancellationToken cancellationToken)
    {
        RegisterLogCommandValidator validator = new();
        validator.ValidateAndThrow(request);

        Log log = Log.New(request.Message);

        await _logRepository.Add(log);
    }
}
