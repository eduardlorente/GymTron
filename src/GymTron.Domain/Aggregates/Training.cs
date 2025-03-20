using GymTron.Domain.Entities;
using GymTron.Domain.Enums;
using GymTron.Domain.ValueObjects;

namespace GymTron.Domain.Aggregates;

public class Training : AggregateRoot<int>
{


    public int RoutineId { get; private set; }
    public int DayOfTheWeek { get; private set; }
    public TrainingDate StartedOn { get; private set; } = new TrainingDate(DateTime.Now);
    public TrainingDate? CompletedOn { get; private set; } = null;
    public List<RoutineItem> PendingWorkout { get; private set; } = [];
    public List<Exercise> CompletedWorkout { get; private set; } = [];


    private Training(int routineId,
                     int dayOfTheWeek,
                     List<RoutineItem> pendingWorkout)
    {
        Id = 0;
        Status.Create();

        RoutineId = routineId;
        DayOfTheWeek = dayOfTheWeek;
        PendingWorkout = pendingWorkout;
    }


    private Training(int trainingId,
                     int routineId,
                     int dayOfTheWeek,
                     DateTime startedOn,
                     DateTime? completedOn,
                     EntityStatusTypes statusType,
                     List<RoutineItem> pendingWorkout,
                     List<Exercise> completedWorkout)
    {
        Id = trainingId;
        RoutineId = routineId;
        DayOfTheWeek = dayOfTheWeek;
        PendingWorkout = pendingWorkout;
        CompletedWorkout = completedWorkout;
        Status = EntityStatus.FromDatabase(statusType, startedOn);
        StartedOn = new(startedOn);

        if (completedOn.HasValue)
        {
            CompletedOn = new(completedOn.Value);
        }
    }


    public static Training CreateAnStartedTraining(int routineId,
                                                   int dayOfTheWeek,
                                                   List<RoutineItem> pendingWorkout)
    {
        return new(routineId,
                   dayOfTheWeek,
                   pendingWorkout);
    }


    public static Training FromDatabase(int trainingId,
                                        int routineId,
                                        int dayOfWeek,
                                        DateTime startedOn,
                                        DateTime? completedOn,
                                        EntityStatusTypes statusType,
                                        List<RoutineItem> pendingWorkout,
                                        List<Exercise> completedWorkout)
    {
        return new(trainingId,
                   routineId,
                   dayOfWeek,
                   startedOn,
                   completedOn,
                   statusType,
                   pendingWorkout,
                   completedWorkout);
    }


    public void CompleteExercise(Exercise exercise)
    {
        bool notExistsExerciseInTraining = !CompletedWorkout.Any(x => x.ExerciseParametersId == exercise.ExerciseParametersId);

        if (notExistsExerciseInTraining)
        {
            CompletedWorkout.Add(exercise);
            PendingWorkout.RemoveAll(x => x.ExerciseParameters.Id == exercise.ExerciseParametersId);
            Status.Update();
        }
    }


    public void Complete()
    {
        if (!Status.IsActive)
        {
            throw new Exception("Training already completed.");
        }

        Status.Update(EntityStatusTypes.COMPLETED);
        CompletedOn = new(DateTime.Now);
    }
}
