using GymTron.Domain.Entities;
using GymTron.Domain.Repositories;
using GymTron.Insfrastructure.Persistence.DAL.MySQL;

namespace GymTron.Insfrastructure.Persistence.Repositories;

internal class LogRepository(ILogDAL logDAL) : ILogRepository
{


    private readonly ILogDAL _logDAL = logDAL;


    public async Task Add(Log entity)
    {
        await _logDAL.Add(entity);
    }


    public async Task<Log?> GetById(int id)
    {
        throw new NotImplementedException();
    }


    public async Task Update(Log entity)
    {
        throw new NotImplementedException();
    }
}
