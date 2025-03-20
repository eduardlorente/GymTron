using GymTron.App.Pages;
using GymTron.App.Services;
using GymTron.App.ViewModels.Entities;

namespace GymTron.App;

public partial class AppShell : Shell
{


    private const string TRAINING_HANDLER = "TrainingHandler";
    private const string TRAININGS_HISTORY_HANDLER = "TrainingsHistoryHandler";
    private const string EXERCISES_HISTORY_HANDLER = "ExercisesHistoryHandler";
    private const string BODY_WEIGHTS_HISTORY_HANDLER = "BodyWeightsHistoryHandler";


    private readonly ITrainingService _trainingService;


    public AppShell(ITrainingService trainingService)
    {
        InitializeComponent();

        _trainingService = trainingService;

        Routing.RegisterRoute(nameof(StartTrainingPage), typeof(StartTrainingPage));
        Routing.RegisterRoute(nameof(CurrentTrainingPage), typeof(CurrentTrainingPage));
        Routing.RegisterRoute(nameof(ExerciseDetailPage), typeof(ExerciseDetailPage));
        Routing.RegisterRoute(nameof(TrainingsHistoryPage), typeof(TrainingsHistoryPage));
        Routing.RegisterRoute(nameof(ExercisesHistoryPage), typeof(ExercisesHistoryPage));
        Routing.RegisterRoute(nameof(BodyWeightsHistoryPage), typeof(BodyWeightsHistoryPage));

        Navigating += OnNavigating!;
    }


    private async void OnNavigating(object sender, ShellNavigatingEventArgs e)
    {
        if (e.Target.Location.OriginalString.Contains(TRAINING_HANDLER))
        {
            e.Cancel();

            TrainingViewModel? currentTraining = await _trainingService.GetCurrentTraining();
            if (currentTraining != null)
            {
                CurrentTrainingPage? currentTrainingPage = App.Services.GetService<CurrentTrainingPage>();
                await Shell.Current.Navigation.PushAsync(currentTrainingPage);
            }
            else
            {
                StartTrainingPage? startTrainingPage = App.Services.GetService<StartTrainingPage>();
                await Shell.Current.Navigation.PushAsync(startTrainingPage);
            }
        }
        else if (e.Target.Location.OriginalString.Contains(TRAININGS_HISTORY_HANDLER))
        {
            e.Cancel();
            await Shell.Current.Navigation.PushAsync(App.Services.GetService<TrainingsHistoryPage>());
        }
        else if (e.Target.Location.OriginalString.Contains(EXERCISES_HISTORY_HANDLER))
        {
            e.Cancel();
            await Shell.Current.Navigation.PushAsync(App.Services.GetService<ExercisesHistoryPage>());
        }
        else if (e.Target.Location.OriginalString.Contains(BODY_WEIGHTS_HISTORY_HANDLER))
        {
            e.Cancel();
            await Shell.Current.Navigation.PushAsync(App.Services.GetService<BodyWeightsHistoryPage>());
        }
    }
}
