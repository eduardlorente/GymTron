namespace GymTron.Insfrastructure.Persistence.DAL.Models;

internal class BodyWeightDALModel
{

    public int Id { get; set; }
    public decimal Weight { get; set; }
    public decimal IMC { get; set; }
    public DateTime CreatedOn { get; set; }
}
