using CommunityToolkit.Maui;
using GymTron.App.Helpers;
using GymTron.App.Pages;
using GymTron.App.Pages.Modals;
using GymTron.App.Services;
using GymTron.App.ViewModels.Pages;
using GymTron.App.ViewModels.Pages.Modals;
using GymTron.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace GymTron.App;

public static class MauiProgram
{


    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        ;

        string connectionString = GetConnectionString(builder);

        ConfigureServices(builder, connectionString);

        return builder.Build();
    }


    private static string GetConnectionString(MauiAppBuilder builder)
    {
#if DEBUG
        string configFileName = "GymTron.App.Resources.Json.appsettings.Development.json";
#else
        string configFileName = "GymTron.App.Resources.Json.appsettings.json";
#endif

        Assembly assembly = typeof(MauiProgram).Assembly;
        using Stream? stream = assembly.GetManifestResourceStream(configFileName);

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(configuration);

        return configuration.GetSection("ConnectionStrings:DefaultConnection").Value ?? string.Empty;
    }


    private static void ConfigureServices(MauiAppBuilder builder, string connectionString)
    {
        //General
        builder.Services.AddSingleton(sp => LoggerFactory.Create(builder =>
        {
#if DEBUG
            builder.AddDebug();
#endif
        }));

        //Layers services
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(connectionString);

        //App services
        builder.Services.AddSingleton<ITrainingService, TrainingService>();
        builder.Services.AddScoped<IRoutineService, RoutineService>();
        builder.Services.AddTransient<IExerciseService, ExerciseService>();
        builder.Services.AddTransient<IBodyWeightService, BodyWeightService>();

        //Domain services
        builder.Services.AddSingleton<IResourceProvider, ResourceProvider>();

        //View models
        builder.Services.AddTransient<StartTrainingPageViewModel>();
        builder.Services.AddTransient<CurrentTrainingPageViewModel>();
        builder.Services.AddTransient<ExerciseDetailPageViewModel>();
        builder.Services.AddTransient<CompleteExerciseModalViewModel>();
        builder.Services.AddTransient<TrainingsHistoryPageViewModel>();
        builder.Services.AddTransient<ExercisesHistoryPageViewModel>();
        builder.Services.AddTransient<BodyWeightsHistoryPageViewModel>();
        builder.Services.AddTransient<AddBodyWeightModalViewModel>();

        //Pages
        builder.Services.AddTransient<StartTrainingPage>();
        builder.Services.AddTransient<CurrentTrainingPage>();
        builder.Services.AddTransient<ExerciseDetailPage>();
        builder.Services.AddTransient<CompleteExerciseModal>();
        builder.Services.AddTransient<TrainingsHistoryPage>();
        builder.Services.AddTransient<ExercisesHistoryPage>();
        builder.Services.AddTransient<BodyWeightsHistoryPage>();
        builder.Services.AddTransient<AddBodyWeightModal>();
    }
}
