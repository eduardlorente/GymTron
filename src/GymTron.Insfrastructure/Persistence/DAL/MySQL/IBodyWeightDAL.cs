using GymTron.Domain.Entities;
using GymTron.Insfrastructure.Persistence.DAL.Models;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal interface IBodyWeightDAL
{
    Task Add(BodyWeight entity);
    Task<IEnumerable<BodyWeightDALModel>> ListAll();
}
