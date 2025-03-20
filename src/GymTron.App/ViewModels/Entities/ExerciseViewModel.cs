using GymTron.Domain.Entities;
using GymTron.Domain.Projections;

namespace GymTron.App.ViewModels.Entities;

public class ExerciseViewModel
{


    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public int TrainingId { get; set; }
    public int ExerciseParametersId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public int DurationInSeconds { get; set; }
    public int CurrentRepetitions { get; set; }
    public List<ObservationViewModel> Observations { get; set; } = [];
    public bool HasWeight => DurationInSeconds == 0;
    public bool HasDuration => DurationInSeconds > 0;


    public ExerciseViewModel()
    {
        //For serialization purposes
    }


    public ExerciseViewModel(Exercise exercise)
    {
        Id = exercise.Id!;
        CreatedOn = exercise.Status.CreatedOn;
        TrainingId = exercise.TrainingId;
        ExerciseParametersId = exercise.ExerciseParametersId;
        Name = exercise.Name;
        Weight = exercise.Weight;
        DurationInSeconds = exercise.DurationInSeconds;
        CurrentRepetitions = exercise.CurrentRepetitions;
        Observations = exercise.Observations.Select(o => new ObservationViewModel(o)).ToList();
    }


    public ExerciseViewModel(ExerciseHistoryProjection exercise)
    {
        Name = exercise.Name;
        CreatedOn = exercise.CreatedOn;
        Weight = exercise.Weight ?? 0;
        CurrentRepetitions = exercise.Repetitions ?? 0;
        DurationInSeconds = exercise.DurationInSeconds ?? 0;
    }


    internal Exercise ToDomain()
        => Exercise.FromDatabase(Id,
                                 TrainingId,
                                 ExerciseParametersId,
                                 Name,
                                 Weight,
                                 DurationInSeconds,
                                 CurrentRepetitions,
                                 CreatedOn,
                                 Observations.Select(o => o.Comment).ToList());
}
