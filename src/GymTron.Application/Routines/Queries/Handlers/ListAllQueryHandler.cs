using GymTron.Application.Base;
using GymTron.Domain.Entities;
using GymTron.Domain.Repositories;
using GymTron.Domain.Services;

namespace GymTron.Application.Routines.Queries.Handlers;

internal class ListAllQueryHandler(IRoutineRepository routineRepository, IExceptionLogger<ListAllQuery> logger)
    : BaseQueryHandler<ListAllQuery, List<Routine>>(logger)
{


    private readonly IRoutineRepository _routineRepository = routineRepository;


    protected override async Task<List<Routine>> HandleQuery(ListAllQuery request, CancellationToken cancellationToken)
    {
        List<Routine> routines = await _routineRepository.ListAll();

        return routines != null && routines.Count != 0
            ? await Task.FromResult(routines)
            : [];
    }
}
