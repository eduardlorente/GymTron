using GymTron.Domain.ValueObjects;

namespace GymTron.App.ViewModels.Entities;

public class ObservationViewModel
{


    public string Comment { get; set; } = string.Empty;


    public ObservationViewModel()
    {
        //For serialization purposes
    }


    public ObservationViewModel(Observation observation)
    {
        Comment = observation.Comment;
    }


    internal Observation ToDomain()
        => new(Comment);
}
