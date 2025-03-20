using GymTron.Domain.Repositories;
using GymTron.Insfrastructure.Persistence.DAL.MySQL;
using GymTron.Insfrastructure.Persistence.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{


    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddMemoryCache();

        // Repositories
        services.AddTransient<ITrainingRepository, TrainingRepository>();
        services.AddTransient<IRoutineRepository, RoutineRepository>();
        services.AddTransient<IExerciseRepository, ExerciseRepository>();
        services.AddTransient<IBodyWeightRepository, BodyWeightRepository>();
        services.AddTransient<ILogRepository, LogRepository>();


        // DALs
        services.AddTransient<IRoutineDAL>(sp => new RoutineDAL(connectionString));
        services.AddTransient<ITrainingDAL>(sp => new TrainingDAL(connectionString));
        services.AddTransient<IExerciseDAL>(sp => new ExerciseDAL(connectionString));
        services.AddTransient<IBodyWeightDAL>(sp => new BodyWeightDAL(connectionString));
        services.AddTransient<ILogDAL>(sp => new LogDAL(connectionString));


        return services;
    }
}
