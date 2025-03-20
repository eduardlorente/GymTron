// DEPRECATED, KEEPED FOR REFERENCE

//using GymTron.Domain.Enums;
//using GymTron.Insfrastructure.Persistence.DAL.Contracts;
//using GymTron.Insfrastructure.Persistence.DAL.Models;

//namespace GymTron.Insfrastructure.Persistence.DAL.GDocs;

//internal class ExerciseParametersDAL(IGoogleSheetsService googleSheetsService) : IExerciseParametersDAL
//{


//    private const string PES = "Pes";


//    private readonly IGoogleSheetsService _googleSheetsService = googleSheetsService;


//    public List<ExerciseParametersDALModel> ListAll()
//    {
//        List<ExerciseParametersDALModel> exerciseParameters = [];

//        IList<IList<object>> persistedExersiseParameters = _googleSheetsService.ReadFile("ExerciseParameters!A2:F");

//        if (persistedExersiseParameters != null)
//        {
//            foreach (IList<object> row in persistedExersiseParameters)
//            {
//                int minRepetitions;
//                int maxRepetitions;

//                string repetitions = row[3].ToString()!;
//                if (repetitions.Contains('-'))
//                {
//                    string[] repetitionsSegments = repetitions.Split('-');
//                    minRepetitions = Convert.ToInt32(repetitionsSegments[0]);
//                    maxRepetitions = Convert.ToInt32(repetitionsSegments[1]);
//                }
//                else
//                {
//                    minRepetitions = Convert.ToInt32(repetitions);
//                    maxRepetitions = minRepetitions;
//                }

//                exerciseParameters.Add(new ExerciseParametersDALModel()
//                {
//                    Name = row[0].ToString() ?? string.Empty,
//                    Pattern = row[1].ToString() ?? string.Empty,
//                    Series = Convert.ToInt32(row[2]),
//                    Repetitions = (minRepetitions, maxRepetitions),
//                    ReplaysInReserve = Convert.ToInt32(row[4]),
//                    Type = (row[5].ToString() ?? string.Empty).ToLower().Equals(PES.ToLower(), StringComparison.CurrentCultureIgnoreCase)
//                        ? ExerciseTypes.WEIGHT
//                        : ExerciseTypes.DURATION
//                });
//            }
//        }

//        return exerciseParameters;
//    }
//}
