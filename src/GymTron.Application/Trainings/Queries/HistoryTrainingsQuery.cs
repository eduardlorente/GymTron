using GymTron.Application.Base;
using GymTron.Domain.Projections;

namespace GymTron.Application.Trainings.Queries;

public class HistoryTrainingsQuery(Guid correlationId) : QueryBase<List<TrainingHistoryProjection>>(correlationId)
{ }
