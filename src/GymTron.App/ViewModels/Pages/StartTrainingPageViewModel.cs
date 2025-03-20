using GymTron.App.Services;
using GymTron.App.ViewModels.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymTron.App.ViewModels.Pages;

public partial class StartTrainingPageViewModel : PageBaseViewModel
{


    private readonly ITrainingService _trainingService;
    private readonly IRoutineService _routineService;


    private ObservableCollection<RoutineViewModel> _routines = [];
    public ObservableCollection<RoutineViewModel> Routines {
        get => _routines;
        set => SetProperty(ref _routines, value);
    }

    private ObservableCollection<int> _routineDays = [];
    public ObservableCollection<int> RoutineDays {
        get => _routineDays;
        set => SetProperty(ref _routineDays, value);
    }

    private ObservableCollection<RoutineItemViewModel> _routineItems = [];
    public ObservableCollection<RoutineItemViewModel> RoutineItems {
        get => _routineItems;
        set => SetProperty(ref _routineItems, value);
    }
 
    private RoutineViewModel? _selectedRoutine = null;
    public RoutineViewModel? SelectedRoutine
    {
        get => _selectedRoutine;
        set
        {
            SetProperty(ref _selectedRoutine, value);
            UpdateRoutineDays();
        }
    }

    private int? _selectedDay;
    public int? SelectedDay
    {
        get => _selectedDay;
        set
        {
            SetProperty(ref _selectedDay, value);
            UpdateRoutineItems();
        }
    }

    private bool _routinesLoaded;
    public bool RoutinesLoaded
    {
        get => _routinesLoaded;
        set => SetProperty(ref _routinesLoaded, value);
    }

    public ICommand StartTrainingCommand { get; }
    public ICommand LoadRoutinesCommand { get; }


    public StartTrainingPageViewModel(ITrainingService trainingService, IRoutineService routineService)
    {
        _trainingService = trainingService;
        _routineService = routineService;

        StartTrainingCommand = new Command(async () =>
        {
            IsBusy = true;
            try
            {
                await OnStartTraining();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        });

        LoadRoutinesCommand = new Command(async () =>
        {
            IsBusy = true;
            try
            {
                await LoadRoutines();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        });
    }


    private async Task LoadRoutines()
    {
        List<RoutineViewModel> routines = await _routineService.ListAllRoutines();
        
        Routines.Clear();
        Routines = new ObservableCollection<RoutineViewModel>(routines);
        RoutinesLoaded = true;
    }


    private void UpdateRoutineDays()
    {
        RoutineDays.Clear();
        RoutineItems.Clear();
        if (SelectedRoutine != null)
        {
            foreach (int day in SelectedRoutine.WorkByDays.Keys)
                RoutineDays.Add(day);
        }
    }


    private void UpdateRoutineItems()
    {
        RoutineItems.Clear();
        if (SelectedRoutine != null && SelectedDay.HasValue)
        {
            List<RoutineItemViewModel> items = SelectedRoutine.WorkByDays[SelectedDay.Value];
            foreach (RoutineItemViewModel item in items)
                RoutineItems.Add(item);
        }
    }


    private async Task OnStartTraining()
    {
        if (SelectedRoutine == null || SelectedDay == null)
            return;

        await _trainingService.StartTraining(SelectedRoutine.Id, SelectedDay.Value);

        await Shell.Current.GoToAsync("//TrainingHandler");

        IReadOnlyList<Page> stack = Shell.Current.Navigation.NavigationStack;
        if (stack.Count > 1)
            Shell.Current.Navigation.RemovePage(stack[stack.Count - 2]);
    }
}
