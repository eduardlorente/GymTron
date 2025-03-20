using GymTron.Insfrastructure.Persistence.DAL.Models;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal interface IRoutineDAL
{
    Task<IEnumerable<RoutineFullDetailsDTO>> ListAll();
    Task<IEnumerable<RoutineFullDetailsDTO>> ListById(int id);
}
