using GymTron.Domain.Entities;
using GymTron.Domain.Enums;

namespace GymTron.App.ViewModels.Entities;

public class ExerciseParametersViewModel
{


    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public RangeViewModel Repetitions { get; set; } = new();
    public int DurationInSeconds { get; set; }
    public int Series { get; set; }
    public int? ReplaysInReserve { get; set; }
    public RangeViewModel RestTimeInSeconds { get; set; }
    public decimal? LastWeight { get; set; }
    public int? LastDurationInSeconds { get; set; }
    public int? LastRepetitions { get; set; }
    public int TypeId { get; set; }
    public List<ObservationViewModel> Observations { get; set; } = [];


    public ExerciseParametersViewModel()
    {
        //For serialization purposes
    }


    public ExerciseParametersViewModel(ExerciseParameters exerciseParameters)
    {
        Id = exerciseParameters.Id;
        Name = exerciseParameters.Name;
        Pattern = exerciseParameters.Pattern;
        Repetitions = new RangeViewModel(exerciseParameters.Repetitions);
        DurationInSeconds = exerciseParameters.DurationInSeconds;
        Series = exerciseParameters.Series;
        ReplaysInReserve = exerciseParameters.ReplaysInReserve;
        RestTimeInSeconds = new RangeViewModel(exerciseParameters.RestTimeInSeconds);
        LastWeight = exerciseParameters.LastWeight;
        LastDurationInSeconds = exerciseParameters.LastDurationInSeconds;
        LastRepetitions = exerciseParameters.LastRepetitions;
        TypeId = (int)exerciseParameters.Type;
        Observations = exerciseParameters.Observations.Select(o => new ObservationViewModel(o)).ToList();
    }


    internal ExerciseParameters ToDomain()
        => ExerciseParameters.FromDatabase(Id,
                                           Name,
                                           Pattern,
                                           Series,
                                           Repetitions.ToDomain(),
                                           DurationInSeconds,
                                           ReplaysInReserve,
                                           RestTimeInSeconds.ToDomain(),
                                           LastWeight,
                                           LastDurationInSeconds,
                                           LastRepetitions,
                                           (ExerciseTypes)TypeId,
                                           Observations.Select(o => o.ToDomain()).ToList());
}
