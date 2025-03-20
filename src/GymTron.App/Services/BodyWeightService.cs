using GymTron.App.ViewModels.Entities;
using GymTron.Application.BodyWeights.Commands;
using GymTron.Application.BodyWeights.Queries;
using GymTron.Domain.Projections;
using MediatR;

namespace GymTron.App.Services;

internal class BodyWeightService(IMediator mediator) : IBodyWeightService
{


    private readonly IMediator _mediator = mediator;


    public async Task RegisterBodyWeight(decimal weight, decimal imc)
    {
        Guid correlationId = Guid.NewGuid();

        RegisterBodyWeightCommand command = new(correlationId, weight, imc);

        await _mediator.Send(command);
    }


    public async Task<List<BodyWeightHistoryItemViewModel>> ListHistory()
    {
        List<BodyWeightHistoryProjection> bodyWeights = await _mediator.Send(new HistoryBodyWeightQuery(Guid.NewGuid()));

        return bodyWeights.Select(bw => new BodyWeightHistoryItemViewModel(bw.Weight, bw.IMC, bw.CreatedOn)).ToList();
    }
}
