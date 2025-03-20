using GymTron.Application.Base;
using GymTron.Domain.Entities;
using GymTron.Domain.Projections;
using GymTron.Domain.Repositories;
using GymTron.Domain.Services;

namespace GymTron.Application.BodyWeights.Queries.Handlers;

internal class HistoryBodyWeightQueryHandler(IBodyWeightRepository bodyWeightRepository, IExceptionLogger<HistoryBodyWeightQuery> logger)
    : BaseQueryHandler<HistoryBodyWeightQuery, List<BodyWeightHistoryProjection>>(logger)
{


    private readonly IBodyWeightRepository _bodyWeightRepository = bodyWeightRepository;


    protected override async Task<List<BodyWeightHistoryProjection>> HandleQuery(HistoryBodyWeightQuery request, CancellationToken cancellationToken)
    {
        List<BodyWeight> trainings = await _bodyWeightRepository.ListAll();

        return await Task.FromResult(
            trainings.Select(t => new BodyWeightHistoryProjection
            {
                Weight = t.Weight,
                IMC = t.IMC,
                CreatedOn = t.Status.CreatedOn
            })
            .OrderByDescending(t => t.CreatedOn)
            .ToList());
    }
}
