using GymTron.Application.DomainServices;
using GymTron.Application.Trainings.Commands;
using GymTron.Domain.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{


    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StartTrainingCommand).Assembly));

        //Services
        services.AddTransient(typeof(IExceptionLogger<>), typeof(ExceptionLogger<>));

        return services;
    }
}
