// DEPRECATED, KEEPED FOR REFERENCE

//using GymTron.Domain.Entities;
//using GymTron.Insfrastructure.Persistence.DAL.Contracts;
//using GymTron.Insfrastructure.Persistence.DAL.Models;

//namespace GymTron.Insfrastructure.Persistence.DAL.GDocs;

//internal class ExerciseDAL(IGoogleSheetsService googleSheetsService) : IExerciseDAL
//{


//    private const string SHEET_NAME = "Exercises";


//    private readonly IGoogleSheetsService _googleSheetsService = googleSheetsService;

//    public IEnumerable<ExerciseDALModel> ListByTrainingId(string trainingId)
//    {
//        List<ExerciseDALModel> exercises = ListAll().ToList();

//        return exercises.Where(e => e.TrainingId == trainingId);
//    }


//    public IEnumerable<ExerciseDALModel> ListAll()
//    {
//        List<ExerciseDALModel> exercises = [];

//        IList<IList<object>> persistedExercises = _googleSheetsService.ReadFile("Exercises!A2:H");

//        if (persistedExercises != null)
//        {
//            foreach (IList<object> row in persistedExercises)
//            {
//                exercises.Add(new ExerciseDALModel()
//                {
//                    Id = row[0].ToString() ?? string.Empty,
//                    TrainingId = row[1].ToString() ?? string.Empty,
//                    Name = row[2].ToString() ?? string.Empty,
//                    Weight = string.IsNullOrWhiteSpace(row[3].ToString()) ? 0 : Convert.ToDecimal(row[3]),
//                    DurationInSeconds = string.IsNullOrWhiteSpace(row[4].ToString()) ? 0 : Convert.ToInt32(row[4]),
//                    Repetitions = Convert.ToInt32(row[5]),
//                    CreatedOn = Convert.ToDateTime(row[6]),
//                    ObservationsCSV = row[7].ToString() ?? string.Empty
//                });
//            }
//        }

//        return exercises;
//    }


//    public IEnumerable<ExerciseDALModel> ListByNames(IEnumerable<string> names)
//    {
//        List<ExerciseDALModel> exercises = ListAll().ToList();

//        return exercises.Where(e => names.Contains(e.Name));
//    }


//    public void Add(Exercise entity)
//    {
//        List<object> fieldsToInsert = ConvertExerciseToFields(entity);

//        _googleSheetsService.AddRowToNextAvailable(SHEET_NAME, fieldsToInsert);
//    }


//    public void AddRange(List<Exercise> exercises)
//    {
//        IList<IList<object>> rowsToInsert = [];
//        foreach (Exercise exercise in exercises)
//        {
//            if (exercise == null || string.IsNullOrWhiteSpace(exercise.Id))
//            {
//                continue;
//            }

//            List<object> fieldsToInsert = ConvertExerciseToFields(exercise);

//            rowsToInsert.Add(fieldsToInsert);
//        }

//        _googleSheetsService.AddRowsToNextAvailable(SHEET_NAME, rowsToInsert);
//    }


//    private static List<object> ConvertExerciseToFields(Exercise entity)
//        =>
//        [
//            entity.Id,
//            entity.TrainingId,
//            entity.Name,
//            entity.Weight,
//            entity.DurationInSeconds,
//            entity.CurrentRepetitions,
//            entity.Status.CreatedOn,
//            string.Join(";", entity.Observations.Select(o => o.Comment))
//        ];
//}
