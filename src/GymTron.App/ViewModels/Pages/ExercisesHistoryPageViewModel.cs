using GymTron.App.Services;
using GymTron.App.ViewModels.Entities;
using System.Collections.ObjectModel;

namespace GymTron.App.ViewModels.Pages;

public partial class ExercisesHistoryPageViewModel : PageBaseViewModel
{


    private readonly IExerciseService _exerciseService;


    private ObservableCollection<string> _distinctExerciseNames;
    public ObservableCollection<string> DistinctExerciseNames
    {
        get => _distinctExerciseNames;
        set => SetProperty(ref _distinctExerciseNames, value);
    }

    private string _selectedExerciseName;
    public string SelectedExerciseName
    {
        get => _selectedExerciseName;
        set
        {
            SetProperty(ref _selectedExerciseName, value);
            LoadExerciseExecutions();
        }
    }

    private ObservableCollection<ExerciseViewModel> _selectedExerciseExecutions;
    public ObservableCollection<ExerciseViewModel> SelectedExerciseExecutions
    {
        get => _selectedExerciseExecutions;
        set => SetProperty(ref _selectedExerciseExecutions, value);
    }

    private bool _exercisesLoaded;
    public bool ExercisesLoaded
    {
        get => _exercisesLoaded;
        set => SetProperty(ref _exercisesLoaded, value);
    }


    public ExercisesHistoryPageViewModel(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;

        LoadDistinctExerciseNames();
    }


    private async void LoadDistinctExerciseNames()
    {
        List<ExerciseViewModel> exercises = await _exerciseService.ListAll();
        DistinctExerciseNames = new ObservableCollection<string>(exercises.Select(e => e.Name).Distinct());
        ExercisesLoaded = true;
    }


    private async void LoadExerciseExecutions()
    {
        if (string.IsNullOrEmpty(SelectedExerciseName))
            return;

        List<ExerciseViewModel> exercises = await _exerciseService.ListAll();
        List<ExerciseViewModel> filteredExercises = [.. exercises
            .Where(e => e.Name == SelectedExerciseName)
            .OrderByDescending(e => e.CreatedOn)];

        SelectedExerciseExecutions = new ObservableCollection<ExerciseViewModel>(filteredExercises);
    }
}
