using GymTron.Application.Base;
using GymTron.Domain.Entities;

namespace GymTron.Application.Routines.Queries;

public class ListAllQuery(Guid correlationId) : QueryBase<List<Routine>>(correlationId)
{ }
