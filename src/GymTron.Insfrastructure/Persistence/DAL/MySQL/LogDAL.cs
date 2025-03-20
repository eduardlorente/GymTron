using Dapper;
using GymTron.Domain.Entities;
using MySql.Data.MySqlClient;
using System.Data;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal class LogDAL(string connectionString) : ILogDAL
{


    public async Task Add(Log entity)
    {
        var parameters = new
        {
            entity.Message,
            entity.Status.CreatedOn
        };

        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"INSERT INTO Logs 
                            (CreatedOn, Message) 
                         VALUES 
                            (@CreatedOn, @Message);";

        await dbConnection.ExecuteAsync(query, parameters);
    }
}
