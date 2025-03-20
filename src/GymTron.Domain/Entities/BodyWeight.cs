using GymTron.Domain.ValueObjects;

namespace GymTron.Domain.Entities;

public class BodyWeight : Entity<int>
{


    public decimal Weight { get; private set; } = 0;
    public decimal IMC { get; private set; } = 0;


    private BodyWeight()
        : base(0)
    {
    }


    private BodyWeight(int id,
                       decimal weight,
                       decimal imc,
                       DateTime createdOn)
        : base(id)
    {
        Weight = weight;
        IMC = imc;
        Status = EntityStatus.FromDatabase(createdOn);
    }


    public static BodyWeight New(decimal weight,
                                 decimal imc,
                                 DateTime createdOn)
    {
        return new BodyWeight(0, weight, imc, createdOn);
    }


    public static BodyWeight FromDatabase(int id,
                                          decimal weight,
                                          decimal imc,
                                          DateTime createdOn)
    {
        return new BodyWeight(id, weight, imc, createdOn);
    }
}
