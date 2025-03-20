using GymTron.App.Services;
using System.Windows.Input;

namespace GymTron.App.ViewModels.Pages.Modals;

public class AddBodyWeightModalViewModel : PageBaseViewModel
{


    public string Weight { get; set; } = string.Empty;
    public string IMC { get; set; } = string.Empty;

    public ICommand AddBodyWeightCommand { get; }

    private readonly Func<Task> _onAddBodyWeight;


    public AddBodyWeightModalViewModel(Func<Task> onAddBodyWeight)
    {
        _onAddBodyWeight = onAddBodyWeight;

        AddBodyWeightCommand = new Command(async () => await OnAddBodyWeight());
    }


    private async Task OnAddBodyWeight()
    {
        IBodyWeightService bodyWeightService = App.Services.GetService<IBodyWeightService>()!;

        if (bodyWeightService != null)
        {
            await bodyWeightService.RegisterBodyWeight(decimal.Parse(Weight), decimal.Parse(IMC));

            await _onAddBodyWeight();
        }
    }
}

