using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GymTron.App.Pages;
using GymTron.App.Services;
using GymTron.App.ViewModels.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymTron.App.ViewModels.Pages;

public partial class CurrentTrainingPageViewModel : PageBaseViewModel
{


    private readonly ITrainingService _trainingService;
    private TrainingViewModel? _currentTraining;
    private CancellationTokenSource? _timerCancellationTokenSource;


    public DateTime StartTime => _currentTraining?.StartedOn.FullDate ?? DateTime.MinValue;

    public string ElapsedTime
    {
        get
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
            string result = string.Empty;

            if (elapsed.Hours > 0)
            {
                result += $"{elapsed.Hours} hores, ";
            }

            if (elapsed.Minutes > 0)
            {
                result += $"{elapsed.Minutes} minuts i ";
            }

            result += $"{elapsed.Seconds} segons";

            return result;
        }
    }

    public ObservableCollection<ExerciseItemViewModel> TrainingExercises { get; }
    public ICommand FinishTrainingCommand { get; }
    public ICommand SelectExerciseCommand { get; }


    public CurrentTrainingPageViewModel(ITrainingService trainingService)
    {
        _trainingService = trainingService;

        TrainingExercises = [];

        LoadCurrentTraining();
        StartTimer();

        FinishTrainingCommand = new Command(async () => await FinishTraining());
        SelectExerciseCommand = new Command<ExerciseItemViewModel>(OnExerciseSelected);
    }


    private async void LoadCurrentTraining()
    {
        _currentTraining = await _trainingService.GetCurrentTraining();
        if (_currentTraining != null)
        {
            List<ExerciseItemViewModel> exercises = [];

            foreach (RoutineItemViewModel item in _currentTraining.PendingWorkout)
            {
                exercises.Add(new ExerciseItemViewModel(item.ExerciseParameters.Id, item.ExerciseParameters.Name, false));
            }

            foreach (ExerciseViewModel exercise in _currentTraining.CompletedWorkout)
            {
                exercises.Add(new ExerciseItemViewModel(exercise.ExerciseParametersId, exercise.Name, true));
            }

            exercises = [.. exercises.OrderByDescending(e => e.IsCompleted)];

            TrainingExercises.Clear();
            foreach (ExerciseItemViewModel exercise in exercises)
            {
                TrainingExercises.Add(exercise);
            }
        }
    }


    private void StartTimer()
    {
        _timerCancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = _timerCancellationTokenSource.Token;

        Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                OnPropertyChanged(nameof(ElapsedTime));
                await Task.Delay(1000, token);
            }
        }, token);
    }


    private async Task FinishTraining()
    {
        bool isConfirmed = await Shell.Current.DisplayAlert(
            "Confirmació",
            "Estàs segur que vols finalitzar l'entrenament?",
            "Sí",
            "No"
        );

        if (isConfirmed)
        {
            if (_currentTraining == null)
                return;

            await _trainingService.FinalizeTraining();
            _timerCancellationTokenSource?.Cancel();

            IToast toast = Toast.Make("Enhorabona, entrenament registrat correctament!");
            await toast.Show();

            await Shell.Current.GoToAsync("//MainPage");
        }
    }


    private async void OnExerciseSelected(ExerciseItemViewModel selectedExercise)
    {
        if (selectedExercise == null || _currentTraining == null)
            return;

        if (_currentTraining.CompletedWorkout.Any(e => e.Name == selectedExercise.Name))
            return;

        await Shell.Current.GoToAsync(
            nameof(ExerciseDetailPage),
            new Dictionary<string, object>
            {
            { "ExerciseParametersId", selectedExercise.ExerciseParametersId }
            });
    }


    internal void ReloadData()
    {
        LoadCurrentTraining();
        StartTimer();
    }
}

