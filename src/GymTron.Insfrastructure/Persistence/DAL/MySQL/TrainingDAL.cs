using Dapper;
using GymTron.Domain.Aggregates;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL.Extensions;
using MySql.Data.MySqlClient;
using System.Data;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal class TrainingDAL(string connectionString) : ITrainingDAL
{


    public async Task Add(Training entity)
    {
        var parameters = new
        {
            entity.RoutineId,
            DayOfWeek = entity.DayOfTheWeek,
            StartedOn = entity.StartedOn.FullDate,
            entity.Status.Status
        };

        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"INSERT INTO Trainings 
                            (RoutineId, DayOfWeek, StartedOn, Status) 
                         VALUES 
                            (@RoutineId, @DayOfWeek, @StartedOn, @Status);";

        await dbConnection.ExecuteAsync(query, parameters);
    }


    public async Task<TrainingDALModel?> GetById(int id)
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"SELECT 
                            Id AS Id,
                            RoutineId AS RoutineId,
                            DayOfWeek AS DayOfWeek,
                            StartedOn AS StartedOn,
                            CompletedOn AS CompletedOn,
                            Status AS StatusType 
                         FROM 
                            Trainings 
                         WHERE 
                            Id = @Id;".ToReadUncommited();

        return await dbConnection.QuerySingleOrDefaultAsync<TrainingDALModel>(query, new { Id = id });
    }


    public async Task<TrainingDALModel?> GetCurrent()
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"SELECT 
                            Id AS Id,
                            RoutineId AS RoutineId,
                            DayOfWeek AS DayOfWeek,
                            StartedOn AS StartedOn,
                            CompletedOn AS CompletedOn,
                            Status AS StatusType
                         FROM 
                            Trainings 
                         WHERE 
                            CompletedOn IS NULL ORDER BY StartedOn DESC LIMIT 1;".ToReadUncommited();

        return await dbConnection.QuerySingleOrDefaultAsync<TrainingDALModel>(query);
    }


    public async Task<List<TrainingDALModel>> ListAll()
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"SELECT 
                            Id AS Id,
                            RoutineId AS RoutineId,
                            DayOfWeek AS DayOfWeek,
                            StartedOn AS StartedOn,
                            CompletedOn AS CompletedOn,
                            Status AS StatusType
                         FROM 
                            Trainings;".ToReadUncommited();

        return (await dbConnection.QueryAsync<TrainingDALModel>(query)).ToList();
    }


    public async Task Update(TrainingDALModel model)
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"UPDATE 
                            Trainings 
                        SET 
                            RoutineId = @RoutineId, 
                            DayOfWeek = @DayOfWeek, 
                            StartedOn = @StartedOn, 
                            CompletedOn = @CompletedOn, 
                            Status = @StatusType 
                        WHERE 
                            Id = @Id";

        await dbConnection.ExecuteAsync(query, model);
    }
}
