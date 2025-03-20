using GymTron.Domain.Entities;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal interface ILogDAL
{
    Task Add(Log entity);
}