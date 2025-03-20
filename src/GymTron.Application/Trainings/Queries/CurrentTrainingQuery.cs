using GymTron.Application.Base;
using GymTron.Domain.Aggregates;

namespace GymTron.Application.Trainings.Queries;

public class CurrentTrainingQuery(Guid correlationId) : QueryBase<Training>(correlationId)
{ }
