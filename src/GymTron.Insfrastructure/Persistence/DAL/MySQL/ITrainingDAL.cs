using GymTron.Domain.Aggregates;
using GymTron.Insfrastructure.Persistence.DAL.Models;

namespace GymTron.Insfrastructure.Persistence.DAL.MySQL;

internal interface ITrainingDAL
{
    Task Add(Training entity);
    Task<TrainingDALModel?> GetById(int id);
    Task<TrainingDALModel?> GetCurrent();
    Task<List<TrainingDALModel>> ListAll();
    Task Update(TrainingDALModel model);
}