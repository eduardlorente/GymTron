using GymTron.Domain.Aggregates;

namespace GymTron.Insfrastructure.Persistence.DAL.Models;

internal class TrainingDALModel
{


    public int Id { get; set; }
    public int RoutineId { get; set; }
    public int DayOfWeek { get; set; }
    public DateTime StartedOn { get; set; }
    public DateTime? CompletedOn { get; set; }
    public int StatusType { get; set; }


    public TrainingDALModel()
    {
        // Empty constructor
    }


    public TrainingDALModel(Training training)
    {
        Id = training.Id!;
        RoutineId = training.RoutineId;
        DayOfWeek = training.DayOfTheWeek;
        StartedOn = training.StartedOn.FullDate;
        CompletedOn = training.CompletedOn?.FullDate;
        StatusType = (int)training.Status.Status;
    }
}
