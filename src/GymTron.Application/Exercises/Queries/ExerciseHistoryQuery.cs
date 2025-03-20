using GymTron.Application.Base;
using GymTron.Domain.Projections;

namespace GymTron.Application.Exercises.Queries;

public class ExerciseHistoryQuery(Guid correlationId) : QueryBase<List<ExerciseHistoryProjection>>(correlationId)
{ }
