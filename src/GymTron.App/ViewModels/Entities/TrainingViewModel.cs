using GymTron.Domain.Aggregates;
using GymTron.Domain.Enums;

namespace GymTron.App.ViewModels.Entities;

public class TrainingViewModel
{


    public string Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public int RoutineId { get; set; }
    public int DayOfTheWeek { get; set; }
    public int StatusId { get; set; }
    public TrainingDateViewModel StartedOn { get; set; }
    public TrainingDateViewModel? CompletedOn { get; set; }
    public List<RoutineItemViewModel> PendingWorkout { get; set; }
    public List<ExerciseViewModel> CompletedWorkout { get; set; }


    public TrainingViewModel()
    {
        //For serialization purposes
    }


    public TrainingViewModel(Training training)
    {
        Id = training.Id.ToString();
        CreatedOn = training.Status.CreatedOn;
        RoutineId = training.RoutineId;
        DayOfTheWeek = training.DayOfTheWeek;
        StatusId = (int)training.Status.Status;
        StartedOn = new TrainingDateViewModel(training.StartedOn.FullDate);
        CompletedOn = training.CompletedOn != null ? new TrainingDateViewModel(training.CompletedOn.FullDate) : null;
        PendingWorkout = training.PendingWorkout.Select(ri => new RoutineItemViewModel(ri)).ToList();
        CompletedWorkout = training.CompletedWorkout.Select(e => new ExerciseViewModel(e)).ToList();

    }


    internal Training ToDomain()
        => Training.FromDatabase(Convert.ToInt32(Id),
                                 RoutineId,
                                 DayOfTheWeek,
                                 StartedOn.FullDate,
                                 CompletedOn?.FullDate,
                                 (EntityStatusTypes)StatusId,
                                 PendingWorkout.Select(ri => ri.ToDomain()).ToList(),
                                 CompletedWorkout.Select(e => e.ToDomain()).ToList());
}
