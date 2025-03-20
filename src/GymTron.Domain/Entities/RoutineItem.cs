namespace GymTron.Domain.Entities;

public class RoutineItem : Entity<int>
{


    public int DayOfWeek { get; private set; }
    public ExerciseParameters ExerciseParameters { get; private set; }
    public bool AlternatingSeries { get; private set; } = false;


    private RoutineItem(int id,
                        int dayOfWeek,
                        ExerciseParameters exerciseParameters,
                        bool alternatingSeries)
    {
        Id = id;
        DayOfWeek = dayOfWeek;
        ExerciseParameters = exerciseParameters;
        AlternatingSeries = alternatingSeries;
    }


    public static RoutineItem FromDatabase(int id,
                                           int dayOfWeek,
                                           ExerciseParameters exerciseParameters,

                                           bool alternatingSeries)
    {
        return new(id,
                   dayOfWeek,
                   exerciseParameters,
                   alternatingSeries);
    }
}
