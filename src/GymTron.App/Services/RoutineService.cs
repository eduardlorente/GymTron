using GymTron.App.ViewModels.Entities;
using GymTron.Application.Routines.Queries;
using GymTron.Domain.Entities;
using MediatR;

namespace GymTron.App.Services;

internal class RoutineService(IMediator mediator) : IRoutineService
{


    private readonly IMediator _mediator = mediator;


    public async Task<List<RoutineViewModel>> ListAllRoutines()
    {
        List<Routine> routines = await _mediator.Send(new ListAllQuery(Guid.NewGuid()));

        return routines.OrderByDescending(r => r.Id)
                       .Select(r => new RoutineViewModel(r))
                       .ToList();
    }
}
