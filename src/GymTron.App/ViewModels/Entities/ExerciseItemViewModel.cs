namespace GymTron.App.ViewModels.Entities;

public class ExerciseItemViewModel(int exerciseParametersId, string name, bool isCompleted)
{


    public int ExerciseParametersId { get; } = exerciseParametersId;
    public string Name { get; } = name;
    public bool IsCompleted { get; } = isCompleted;
}
