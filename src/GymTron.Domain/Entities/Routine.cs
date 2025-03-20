namespace GymTron.Domain.Entities;

public class Routine : Entity<int>
{


    public string Name { get; private set; } = string.Empty;
    private List<RoutineItem> RoutineExercises { get; set; } = [];
    public Dictionary<int, List<RoutineItem>> WorkByDays
    {
        get
        {
            return RoutineExercises
                .GroupBy(s => s.DayOfWeek)
                .ToDictionary(s => s.Key, s => s.ToList());
        }
    }


    private Routine(int id,
                    string name,
                    List<RoutineItem> routineExercises)
        : base(id)
    {
        Name = name;
        RoutineExercises = routineExercises;
    }


    public static Routine FromDatabase(int id,
                                       string name,
                                       List<RoutineItem> routineExercises)
    {
        return new(id, name, routineExercises);
    }
}
