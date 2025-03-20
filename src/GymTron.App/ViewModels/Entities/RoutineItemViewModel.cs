using GymTron.Domain.Entities;

namespace GymTron.App.ViewModels.Entities;

public class RoutineItemViewModel
{


    public int Id { get; set; }
    public int DayOfWeek { get; set; }
    public ExerciseParametersViewModel ExerciseParameters { get; set; }
    public bool AlternatingSeries { get; set; }


    public RoutineItemViewModel()
    {
        //For serialization purposes
    }


    public RoutineItemViewModel(RoutineItem routineItem)
    {
        Id = routineItem.Id;
        DayOfWeek = routineItem.DayOfWeek;
        ExerciseParameters = new ExerciseParametersViewModel(routineItem.ExerciseParameters);
        AlternatingSeries = routineItem.AlternatingSeries;
    }


    internal RoutineItem ToDomain()
        => RoutineItem.FromDatabase(Id,
                                    DayOfWeek,
                                    ExerciseParameters.ToDomain(),
                                    AlternatingSeries);
}
