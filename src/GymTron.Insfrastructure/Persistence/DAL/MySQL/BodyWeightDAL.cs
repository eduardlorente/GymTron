using Dapper;
using GymTron.Domain.Entities;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL.Extensions;
using MySql.Data.MySqlClient;
using System.Data;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal class BodyWeightDAL(string connectionString) : IBodyWeightDAL
{


    public async Task Add(BodyWeight entity)
    {
        var parameters = new
        {
            entity.Weight,
            entity.IMC,
            entity.Status.CreatedOn
        };

        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"INSERT INTO BodyWeights 
                            (Weight, IMC, CreatedOn) 
                         VALUES 
                            (@Weight, @IMC, @CreatedOn);";

        await dbConnection.ExecuteAsync(query, parameters);
    }


    public async Task<IEnumerable<BodyWeightDALModel>> ListAll()
    {
        using IDbConnection dbConnection = new MySqlConnection(connectionString);

        string query = @"SELECT 
                            BW.Id AS Id,
                            BW.Weight AS Weight,
                            BW.IMC AS IMC,
                            BW.CreatedOn AS CreatedOn
                         FROM 
                            BodyWeights BW;".ToReadUncommited();

        return await dbConnection.QueryAsync<BodyWeightDALModel>(query);
    }
}
