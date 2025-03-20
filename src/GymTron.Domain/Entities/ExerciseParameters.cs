using GymTron.Domain.Enums;
using GymTron.Domain.ValueObjects;

namespace GymTron.Domain.Entities;

public class ExerciseParameters : Entity<int>
{


    public string Name { get; private set; } = string.Empty;
    public string Pattern { get; private set; } = string.Empty;
    public int Series { get; private set; }
    public (int Min, int Max) Repetitions { get; private set; }
    public int DurationInSeconds { get; private set; }
    public int? ReplaysInReserve { get; private set; }
    public (int Min, int Max) RestTimeInSeconds { get; private set; }
    public decimal? LastWeight { get; private set; }
    public int? LastDurationInSeconds { get; private set; }
    public int? LastRepetitions { get; private set; }
    public ExerciseTypes Type { get; private set; }
    public List<Observation> Observations { get; private set; } = [];


    private ExerciseParameters(int id,
                               string name,
                               string pattern,
                               int series,
                               (int Min, int Max) repetitions,
                               int durationInSeconds,
                               int? replaysInReserve,
                               (int Min, int Max) restTimeInSeconds,
                               decimal? lastWeight,
                               int? lastDurationInSeconds,
                               int? lastRepetitions,
                               ExerciseTypes type,
                               List<Observation> observations)
    {
        Id = id;
        Name = name;
        Pattern = pattern;
        Series = series;
        Repetitions = repetitions;
        DurationInSeconds = durationInSeconds;
        ReplaysInReserve = replaysInReserve;
        RestTimeInSeconds = restTimeInSeconds;
        LastWeight = lastWeight;
        LastDurationInSeconds = lastDurationInSeconds;
        LastRepetitions = lastRepetitions;
        Type = type;
        Observations = observations;
    }


    public static ExerciseParameters FromDatabase(int id,
                                                  string name,
                                                  string pattern,
                                                  int series,
                                                  (int Min, int Max) repetitions,
                                                  int durationInSeconds,
                                                  int? replaysInReserve,
                                                  (int Min, int Max) restTimeInSeconds,
                                                  decimal? lastWeight,
                                                  int? lastDurationInSeconds,
                                                  int? lastRepetitions,
                                                  ExerciseTypes type,
                                                  List<Observation> observations)
    {
        return new ExerciseParameters(id,
                                      name,
                                      pattern,
                                      series,
                                      repetitions,
                                      durationInSeconds,
                                      replaysInReserve,
                                      restTimeInSeconds,
                                      lastWeight,
                                      lastDurationInSeconds,
                                      lastRepetitions,
                                      type,
                                      observations);
    }
}
