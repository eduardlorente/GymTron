using Dapper;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL.Extensions;
using MySql.Data.MySqlClient;
using System.Data;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal class RoutineDAL(string connectionString) : IRoutineDAL
{


    public async Task<IEnumerable<RoutineFullDetailsDTO>> ListAll()
    {
        return await ListAllWithFullInfo(null);
    }


    public async Task<IEnumerable<RoutineFullDetailsDTO>> ListById(int id)
    {
        return await ListAllWithFullInfo(id);
    }


    private async Task<IEnumerable<RoutineFullDetailsDTO>> ListAllWithFullInfo(int? routineId)
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string sql = @"SELECT 
                           r.Id AS RoutineId, 
                           r.Name AS RoutineName,
                       
                           ri.Id AS RoutineItemId, 
                           ri.DayOfWeek, 
                           ri.ExerciseParametersId, 
                           ri.MinRestTimeInSeconds, 
                           ri.MaxRestTimeInSeconds, 
                           ri.AlternatingSeries, 
                           ri.Active,
                           ri.Series, 
                           ri.RepetitionsMin, 
                           ri.RepetitionsMax, 
                           ri.Duration, 
                       
                           ep.Id AS ExerciseParametersId,
                           ep.Name AS ExerciseName, 
                           ep.Pattern, 
                           ep.ReplaysInReserve, 
                           ep.TypeId,
                       
                           le.Weight AS LastWeight,
                           le.Duration AS LastDuration,
                           le.Repetitions AS LastRepetitions,
                           le.Observations AS LastObservations
                       
                       FROM Routines r
                       LEFT JOIN RoutineItems ri ON r.Id = ri.RoutineId
                       LEFT JOIN ExerciseParameters ep ON ri.ExerciseParametersId = ep.Id
                       LEFT JOIN Exercises le 
                           ON ep.Id = le.ExerciseParametersId 
                           AND le.CreatedOn = (SELECT MAX(e2.CreatedOn) 
                                               FROM Exercises e2 
                                               WHERE e2.ExerciseParametersId = ep.Id)
                       
                       WHERE (@RoutineId IS NULL OR r.Id = @RoutineId)
                       ORDER BY r.Id DESC, ri.DayOfWeek ASC, ri.Order ASC;".ToReadUncommited();

        var parameters = new { RoutineId = routineId };

        IEnumerable<RoutineFullDetailsDTO> result = await dbConnection.QueryAsync<RoutineFullDetailsDTO>(sql, parameters);

        return result.Distinct();
    }
}
