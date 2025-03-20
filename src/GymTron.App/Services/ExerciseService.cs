using GymTron.App.ViewModels.Entities;
using GymTron.Application.Exercises.Queries;
using MediatR;

namespace GymTron.App.Services;

internal class ExerciseService(IMediator mediator) : IExerciseService
{


    private readonly IMediator _mediator = mediator;


    public async Task<List<ExerciseViewModel>> ListAll()
    {
        List<Domain.Projections.ExerciseHistoryProjection> exercises = await _mediator.Send(new ExerciseHistoryQuery(Guid.NewGuid()));

        return exercises.Select(x => new ExerciseViewModel(x)).ToList();
    }
}
