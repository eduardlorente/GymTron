using GymTron.App.Pages;
using GymTron.App.Pages.Modals;
using GymTron.App.Services;
using GymTron.App.ViewModels.Entities;
using GymTron.App.ViewModels.Pages.Modals;
using GymTron.Domain.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymTron.App.ViewModels.Pages;

public class ExerciseDetailPageViewModel : PageBaseViewModel, IQueryAttributable
{


    private readonly ITrainingService _trainingService;


    private int _exerciseParametersId;

    private string exerciseName = string.Empty;
    public string ExerciseName
    {
        get => exerciseName;
        set => SetProperty(ref exerciseName, value);
    }

    private string repetitionsTitle = string.Empty;
    public string RepetitionsTitle
    {
        get => repetitionsTitle;
        set => SetProperty(ref repetitionsTitle, value);
    }

    private string repetitions = string.Empty;
    public string Repetitions
    {
        get => repetitions;
        set => SetProperty(ref repetitions, value);
    }

    private string series = string.Empty;
    public string Series
    {
        get => series;
        set => SetProperty(ref series, value);
    }

    private bool alternatingSeries = false;
    public bool AlternatingSeries
    {
        get => alternatingSeries;
        set => SetProperty(ref alternatingSeries, value);
    }

    private string pattern = string.Empty;
    public string Pattern
    {
        get => pattern;
        set => SetProperty(ref pattern, value);
    }

    private string replaysInReserve = string.Empty;
    public string ReplaysInReserve
    {
        get => replaysInReserve;
        set => SetProperty(ref replaysInReserve, value);
    }

    private string _restTime = string.Empty;
    public string RestTime
    {
        get => _restTime;
        set => SetProperty(ref _restTime, value);
    }

    private decimal _lastWeight;
    private string lastWeightWithSufix = string.Empty;
    public string? LastWeightWithSuffix
    {
        get => lastWeightWithSufix;
        set => SetProperty(ref lastWeightWithSufix, value);
    }

    private string lastRepetitions = string.Empty;
    public string? LastRepetitions
    {
        get => lastRepetitions;
        set => SetProperty(ref lastRepetitions, value);
    }

    private int _lastDurationInSeconds;
    private string lastDurationWithSufix = string.Empty;
    public string? LastDurationWithSufix
    {
        get => lastDurationWithSufix;
        set => SetProperty(ref lastDurationWithSufix, value);
    }

    private bool isDurationExercise;
    public bool IsDurationExercise
    {
        get => isDurationExercise;
        set => SetProperty(ref isDurationExercise, value);
    }

    private ObservableCollection<string> _observations;
    public ObservableCollection<string> Observations
    {
        get => _observations;
        set => SetProperty(ref _observations, value);
    }

    public ICommand CompleteExerciseCommand { get; }


    public ExerciseDetailPageViewModel(ITrainingService trainingService)
    {
        _trainingService = trainingService;
        CompleteExerciseCommand = new Command(async () => await OnCompleteExercise());
    }


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ExerciseParametersId", out object? id) && id is int exerciseParamtersId)
        {
            _exerciseParametersId = exerciseParamtersId;
            await LoadExerciseDetails();
        }
    }


    private async Task LoadExerciseDetails()
    {
        TrainingViewModel? currentTraining = await _trainingService.GetCurrentTraining();

        if (currentTraining == null)
        {
            await Shell.Current.DisplayAlert("Error", "No hi ha cap entrenament en curs.", "D'acord");
            return;
        }

        RoutineItemViewModel? routineItem = currentTraining
            .PendingWorkout
            .FirstOrDefault(item => item.ExerciseParameters.Id == _exerciseParametersId);

        if (routineItem != null)
        {
            ExerciseParametersViewModel? exercise = routineItem?.ExerciseParameters;

            if (exercise != null)
            {
                ExerciseName = exercise.Name;
                IsDurationExercise = exercise.TypeId == (int)ExerciseTypes.DURATION;
                Series = exercise.Series.ToString();
                Pattern = exercise.Pattern;
                AlternatingSeries = routineItem.AlternatingSeries;
                Observations = new ObservableCollection<string>(exercise.Observations.Select(o => o.Comment));
                RestTime = BuildRestTimeLiteral(routineItem);

                if (IsDurationExercise)
                {
                    _lastDurationInSeconds = exercise.LastDurationInSeconds ?? 0;
                    LastDurationWithSufix = $"{_lastDurationInSeconds} segons";
                    RepetitionsTitle = "Duració:";
                    Repetitions = $"{exercise.DurationInSeconds} segons";
                }
                else
                {
                    _lastWeight = exercise.LastWeight ?? 0;
                    LastWeightWithSuffix = $"{_lastWeight} kg";
                    LastRepetitions = exercise.LastRepetitions.HasValue ? exercise.LastRepetitions.Value.ToString() : "0";
                    RepetitionsTitle = "Repeticions:";
                    Repetitions = $"{exercise.Repetitions.Min} - {exercise.Repetitions.Max}";
                    ReplaysInReserve = exercise.ReplaysInReserve.HasValue ? exercise.ReplaysInReserve.Value.ToString() : "0";
                }
            }
        }
    }


    private async Task OnCompleteExercise()
    {
        CompleteExerciseModalViewModel model;
        static async Task onCompleted()
        {
            await Shell.Current.Navigation.PopModalAsync();
            await Shell.Current.Navigation.PopAsync();

            if (Shell.Current.CurrentPage is CurrentTrainingPage currentPage)
            {
                (currentPage.BindingContext as CurrentTrainingPageViewModel)?.ReloadData();
            }
        }

        if (IsDurationExercise)
        {
            model = CompleteExerciseModalViewModel.CreateDurationExercise(_exerciseParametersId,
                                                                          ExerciseName,
                                                                          _lastDurationInSeconds,
                                                                          onCompleted);
        }
        else
        {
            model = CompleteExerciseModalViewModel.CreateWeightExercise(_exerciseParametersId,
                                                                        ExerciseName,
                                                                        _lastWeight,
                                                                        int.Parse(LastRepetitions ?? "0"),
                                                                        onCompleted);
        }

        await Shell.Current.Navigation.PushModalAsync(new CompleteExerciseModal
        {
            BindingContext = model
        });
    }


    private static string BuildRestTimeLiteral(RoutineItemViewModel routineItem)
    {
        if (routineItem.ExerciseParameters.RestTimeInSeconds.Min == routineItem.ExerciseParameters.RestTimeInSeconds.Max)
        {
            return $"{routineItem.ExerciseParameters.RestTimeInSeconds.Min} segons";
        }
        else if (routineItem.ExerciseParameters.RestTimeInSeconds.Min == 0 && routineItem.ExerciseParameters.RestTimeInSeconds.Max > 0)
        {
            return $"{routineItem.ExerciseParameters.RestTimeInSeconds.Max} segons";
        }
        else if (routineItem.ExerciseParameters.RestTimeInSeconds.Max == 0 && routineItem.ExerciseParameters.RestTimeInSeconds.Min > 0)
        {
            return $"{routineItem.ExerciseParameters.RestTimeInSeconds.Min} segons";
        }

        return $"{routineItem.ExerciseParameters.RestTimeInSeconds.Min} - {routineItem.ExerciseParameters.RestTimeInSeconds.Max} segons";
    }
}

