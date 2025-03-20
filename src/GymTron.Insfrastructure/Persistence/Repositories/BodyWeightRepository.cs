using GymTron.Domain.Entities;
using GymTron.Domain.Repositories;
using GymTron.Insfrastructure.Persistence.DAL.Models;
using GymTron.Insfrastructure.Persistence.DAL.MySQL;

namespace GymTron.Insfrastructure.Persistence.Repositories;

internal class BodyWeightRepository(IBodyWeightDAL bodyWeightDAL) : IBodyWeightRepository
{


    private readonly IBodyWeightDAL _bodyWeightDAL = bodyWeightDAL;


    public async Task Add(BodyWeight entity)
    {
        await _bodyWeightDAL.Add(entity);
    }


    public Task<BodyWeight?> GetById(int id)
    {
        throw new NotImplementedException();
    }


    public async Task<List<BodyWeight>> ListAll()
    {
        IEnumerable<BodyWeightDALModel> bodyWeights = await _bodyWeightDAL.ListAll();

        return bodyWeights.Select(bw => BodyWeight.FromDatabase(bw.Id,
                                                                bw.Weight,
                                                                bw.IMC,
                                                                bw.CreatedOn))
                          .ToList();
    }


    public Task Update(BodyWeight entity)
    {
        throw new NotImplementedException();
    }
}
