using GymTron.Application.Base;
using GymTron.Domain.Projections;

namespace GymTron.Application.BodyWeights.Queries;

public class HistoryBodyWeightQuery(Guid correlationId) : QueryBase<List<BodyWeightHistoryProjection>>(correlationId)
{ }
