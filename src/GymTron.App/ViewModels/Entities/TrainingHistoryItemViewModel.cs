namespace GymTron.App.ViewModels.Entities;

public class TrainingHistoryItemViewModel(int year, string month, int count)
{


    public int Year { get; set; } = year;
    public string Month { get; set; } = month;
    public int Count { get; set; } = count;
}
