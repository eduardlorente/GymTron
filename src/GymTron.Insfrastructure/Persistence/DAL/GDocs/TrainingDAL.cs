// DEPRECATED, KEEPED FOR REFERENCE

//using GymTron.Domain.Aggregates;
//using GymTron.Insfrastructure.Persistence.DAL.Contracts;
//using GymTron.Insfrastructure.Persistence.DAL.Models;

//namespace GymTron.Insfrastructure.Persistence.DAL.GDocs;

//internal class TrainingDAL(IGoogleSheetsService googleSheetsService) : ITrainingDAL
//{


//    private const string SHEET_NAME = "Trainings";


//    private readonly IGoogleSheetsService _googleSheetsService = googleSheetsService;

//    public async Task<TrainingDALModel?> GetById(string id)
//    {
//        List<TrainingDALModel> trainings = await ListAll();

//        return trainings.FirstOrDefault(t => t.Id == id);
//    }


//    public async Task<TrainingDALModel?> GetCurrent()
//    {
//        List<TrainingDALModel> trainings = await ListAll();

//        return trainings != null && trainings.Count > 0
//            ? trainings.Where(t => t.StatusType == Domain.Enums.EntityStatusTypes.ACTIVE.GetHashCode()).FirstOrDefault()
//            : null;
//    }


//    public async Task<List<TrainingDALModel>> ListAll()
//        => await ListAllFromRepo();


//    public async Task Add(Training entity)
//    {
//        List<object> fieldsToInsert =
//        [
//            entity.Id ?? string.Empty,
//            entity.RoutineId,
//            entity.DayOfTheWeek,
//            entity.StartedOn.FullDate,
//            entity.CompletedOn?.FullDate,
//            (int)entity.Status.Status
//        ];

//        _googleSheetsService.AddRowToNextAvailable(SHEET_NAME, fieldsToInsert);
//    }


//    public async Task Update(TrainingDALModel model)
//    {
//        _googleSheetsService.UpdateRowById(SHEET_NAME,
//                                           model.Id,
//                                           [
//                                               model.Id,
//                                               model.RoutineId,
//                                               model.DayOfWeek,
//                                               model.StartedOn,
//                                               model.CompletedOn ?? DateTime.MinValue,
//                                               model.StatusType
//                                           ]);
//    }


//    private async Task<List<TrainingDALModel>> ListAllFromRepo()
//    {
//        List<TrainingDALModel> trainings = [];

//        IList<IList<object>> persistedTrainings = _googleSheetsService.ReadFile($"{SHEET_NAME}!A2:F");

//        if (persistedTrainings != null)
//        {
//            foreach (IList<object> row in persistedTrainings)
//            {
//                DateTime? completedOn = null;

//                object completedOnRow = row[4];
//                if (completedOnRow != null)
//                {
//                    if (!string.IsNullOrEmpty(completedOnRow.ToString()))
//                    {
//                        completedOn = Convert.ToDateTime(completedOnRow);
//                    }
//                }

//                trainings.Add(new TrainingDALModel()
//                {
//                    Id = row[0].ToString() ?? string.Empty,
//                    RoutineId = Convert.ToInt32(row[1]),
//                    DayOfWeek = Convert.ToInt32(row[2]),
//                    StartedOn = Convert.ToDateTime(row[3]),
//                    CompletedOn = completedOn,
//                    StatusType = Convert.ToInt32(row[5])
//                });
//            }
//        }

//        return await Task.FromResult(trainings);
//    }
//}
