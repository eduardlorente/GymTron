using GymTron.Domain.Entities;
using GymTron.Domain.Enums;
using GymTron.Domain.Repositories;
using GymTron.Domain.ValueObjects;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL;
using Microsoft.Extensions.Caching.Memory;

namespace GymTron.Insfrastructure.Persistence.Repositories;

internal class RoutineRepository(IRoutineDAL routineDAL,
                                 IMemoryCache memoryCache) : IRoutineRepository
{


    private const string ALL_ROUTINES_CACHE_KEY = "AllRoutinesCacheKey";


    private readonly IRoutineDAL _routineDAL = routineDAL;
    private readonly IMemoryCache _memoryCache = memoryCache;


    public async Task Add(Routine entity)
    {
        throw new NotImplementedException();
    }


    public async Task<List<Routine>> ListAll()
    {
        IEnumerable<RoutineFullDetailsDTO> routines = await FetchAllRoutines();

        return BuildRoutineFromData(routines);
    }


    public async Task<Routine?> GetById(int id)
    {
        IEnumerable<RoutineFullDetailsDTO> routines = await FetchAllRoutines();

        return BuildRoutineFromData(routines)
            .FirstOrDefault(r => r.Id == id);
    }


    public async Task Update(Routine entity)
    {
        throw new NotImplementedException();
    }


    private async Task<IEnumerable<RoutineFullDetailsDTO>> FetchAllRoutines()
    {
        if (!_memoryCache.TryGetValue(ALL_ROUTINES_CACHE_KEY, out IEnumerable<RoutineFullDetailsDTO>? routines))
        {
            routines = await _routineDAL.ListAll();

            _memoryCache.Set(ALL_ROUTINES_CACHE_KEY, routines, TimeSpan.FromHours(3));
        }

        return routines ?? [];
    }


    private static List<Routine> BuildRoutineFromData(IEnumerable<RoutineFullDetailsDTO> routinesData)
        => routinesData
            .GroupBy(r => r.RoutineId)
            .Select(g => Routine.FromDatabase(
                            g.Key,
                            g.First().RoutineName,
                            [.. g.Select(item => RoutineItem.FromDatabase(
                                                item.RoutineItemId,
                                                item.DayOfWeek,
                                                ExerciseParameters.FromDatabase(
                                                    item.ExerciseParametersId,
                                                    item.ExerciseName,
                                                    item.Pattern,
                                                    item.Series,
                                                    (item.RepetitionsMin, item.RepetitionsMax),
                                                    item.Duration ?? 0,
                                                    item.ReplaysInReserve,
                                                    (item.MinRestTimeInSeconds, item.MaxRestTimeInSeconds ?? 0),
                                                    item.LastWeight,
                                                    item.LastDuration,
                                                    item.LastRepetitions,
                                                    (ExerciseTypes)item.TypeId,
                                                    !string.IsNullOrEmpty(item.LastObservations)
                                                        ? item.LastObservations.Split(';').Select(o => new Observation(o)).ToList()
                                                        : []),
                                                item.AlternatingSeries))
                            ]
                            )).ToList();
}
