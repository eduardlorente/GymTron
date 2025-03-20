using GymTron.Domain.Entities;

namespace GymTron.App.ViewModels.Entities;

public class RoutineViewModel
{


    public int Id { get; set; }
    public string Name { get; set; }
    public Dictionary<int, List<RoutineItemViewModel>> WorkByDays { get; set; }


    public RoutineViewModel()
    {
        //For serialization purposes
    }


    public RoutineViewModel(Routine routine)
    {
        Id = routine.Id;
        Name = routine.Name;
        WorkByDays = routine.WorkByDays.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Select(ri => new RoutineItemViewModel(ri)).ToList()
        );
    }


    internal Routine ToDomain()
        => Routine.FromDatabase(Id,
                               Name,
                               WorkByDays.SelectMany(kvp => kvp.Value).Select(ri => ri.ToDomain()).ToList());
}
