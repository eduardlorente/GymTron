namespace GymTron.App;

public partial class MainPage : ContentPage
{


    public MainPage()
    {
        InitializeComponent();
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }


    private void Training_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//TrainingHandler");
    }


    private void TrainingHistory_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//TrainingsHistoryHandler");
    }


    private void ExerciseHistory_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ExercisesHistoryHandler");
    }


    private void BodyWeightHistory_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//BodyWeightsHistoryHandler");
    }


    //private void Settings_Clicked(object sender, EventArgs e)
    //{
    //    Shell.Current.GoToAsync("//Settings");
    //}
}
