using GymTron.App.Services;
using GymTron.App.ViewModels.Entities;
using System.Collections.ObjectModel;

namespace GymTron.App.ViewModels.Pages;

public partial class TrainingsHistoryPageViewModel : PageBaseViewModel
{


    private readonly ITrainingService _trainingService;


    public ObservableCollection<TrainingHistoryItemViewModel> TrainingHistoryItems { get; set; } = [];


    public TrainingsHistoryPageViewModel(ITrainingService trainingService)
    {
        _trainingService = trainingService;

        LoadTrainingHistory();
    }


    private async void LoadTrainingHistory()
    {
        List<TrainingHistoryItemViewModel> historyItems = await _trainingService.ListHistory();

        TrainingHistoryItems.Clear();
        foreach (TrainingHistoryItemViewModel item in historyItems)
        {
            TrainingHistoryItems.Add(item);
        }
    }
}
