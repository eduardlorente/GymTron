using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GymTron.App.Pages.Modals;
using GymTron.App.Services;
using GymTron.App.ViewModels.Entities;
using GymTron.App.ViewModels.Pages.Modals;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymTron.App.ViewModels.Pages;

public partial class BodyWeightsHistoryPageViewModel : PageBaseViewModel
{


    private readonly IBodyWeightService _bodyWeightService;


    public ObservableCollection<BodyWeightHistoryItemViewModel> BodyWeightsHistoryItems { get; set; } = [];

    public ICommand AddBodyWeightCommand { get; }


    public BodyWeightsHistoryPageViewModel(IBodyWeightService bodyWeightService)
    {
        _bodyWeightService = bodyWeightService;

        LoadTrainingHistory();

        AddBodyWeightCommand = new Command(async () => await OnAddBodyWeight());
    }


    private async void LoadTrainingHistory()
    {
        List<BodyWeightHistoryItemViewModel> historyItems = await _bodyWeightService.ListHistory();

        BodyWeightsHistoryItems.Clear();

        for (int i = 0; i < historyItems.Count; i++)
        {
            BodyWeightHistoryItemViewModel item = historyItems[i];
            BodyWeightHistoryItemViewModel? nextItem = i < historyItems.Count - 1 ? historyItems[i + 1] : null;

            item.SetColor(nextItem!);
            BodyWeightsHistoryItems.Add(item);
        }
    }


    private async Task OnAddBodyWeight()
    {
        AddBodyWeightModalViewModel model;

        static async Task onAddBodyWeight()
        {
            await Shell.Current.Navigation.PopModalAsync();
            await Shell.Current.Navigation.PopAsync();

            IToast toast = Toast.Make("Enhorabona, nova mesura registrada correctament!");
            await toast.Show();
        }

        model = new AddBodyWeightModalViewModel(onAddBodyWeight);

        await Shell.Current.Navigation.PushModalAsync(new AddBodyWeightModal
        {
            BindingContext = model
        });
    }
}
