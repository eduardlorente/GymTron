using Dapper;
using GymTron.Domain.Entities;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL.Extensions;
using MySql.Data.MySqlClient;
using System.Data;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal class ExerciseDAL(string connectionString) : IExerciseDAL
{


    public async Task Add(Exercise entity)
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"INSERT INTO Exercises 
                            (TrainingId, ExerciseParametersId, Weight, Duration, Repetitions, CreatedOn, Observations) 
                         VALUES 
                            (@TrainingId, @ExerciseParametersId, @Weight, @Duration, @Repetitions, @CreatedOn, @Observations);";

        await dbConnection.ExecuteAsync(query, entity);
    }


    public async Task AddRange(List<Exercise> exercises)
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"INSERT INTO Exercises 
                        (TrainingId, ExerciseParametersId, Weight, Duration, Repetitions, CreatedOn, Observations) 
                     VALUES 
                        (@TrainingId, @ExerciseParametersId, @Weight, @Duration, @Repetitions, @CreatedOn, @Observations);";

        var parameters = exercises.Select(e => new
        {
            e.TrainingId,
            e.ExerciseParametersId,
            e.Weight,
            Duration = e.DurationInSeconds,
            Repetitions = e.CurrentRepetitions,
            e.Status.CreatedOn,
            Observations = string.Join(";", e.Observations.Select(o => o.Comment))
        });

        await dbConnection.ExecuteAsync(query, parameters);
    }


    public async Task<IEnumerable<ExerciseDALModel>> ListByTrainingId(int trainingId)
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"SELECT 
                            E.Id AS Id,
                            E.TrainingId AS TrainingId,
                            E.ExerciseParametersId AS ExerciseParametersId,
                            EP.Name AS Name,
                            E.Weight AS Weight,
                            E.Duration AS DurationInSeconds,
                            E.Repetitions AS Repetitions,
                            E.CreatedOn AS CreatedOn,
                            E.Observations AS ObservationsCSV
                         FROM 
                            Exercises E
                         LEFT JOIN 
                            ExerciseParameters EP ON E.ExerciseParametersId = EP.Id
                         WHERE 
                            E.TrainingId = @TrainingId;".ToReadUncommited();

        return await dbConnection.QueryAsync<ExerciseDALModel>(query, new { TrainingId = trainingId });
    }


    public async Task<IEnumerable<ExerciseDALModel>> ListAll()
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"SELECT 
                            E.Id AS Id,
                            E.TrainingId AS TrainingId,
                            EP.Name AS Name,
                            E.Weight AS Weight,
                            E.Duration AS DurationInSeconds,
                            E.Repetitions AS Repetitions,
                            E.CreatedOn AS CreatedOn,
                            E.Observations AS ObservationsCSV
                         FROM 
                            Exercises E
                         LEFT JOIN 
                            ExerciseParameters EP ON E.ExerciseParametersId = EP.Id;".ToReadUncommited();

        return await dbConnection.QueryAsync<ExerciseDALModel>(query);
    }
}
