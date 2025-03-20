// DEPRECATED, KEEPED FOR REFERENCE

//using GymTron.Insfrastructure.Persistence.DAL.Contracts;
//using GymTron.Insfrastructure.Persistence.DAL.Models;

//namespace GymTron.Insfrastructure.Persistence.DAL.GDocs;

//internal class RoutineItemsDAL(IGoogleSheetsService googleSheetsService) : IRoutineItemsDAL
//{


//    private const string YES = "Si";


//    private readonly IGoogleSheetsService _googleSheetsService = googleSheetsService;

//    public IEnumerable<IGrouping<int, RoutineItemDALModel>> GetAllGrouppedByRoutineId()
//    {
//        List<RoutineItemDALModel> routineItems = [];

//        IList<IList<object>> persistedRoutineItems = _googleSheetsService.ReadFile("RoutineItems!A2:G");

//        if (persistedRoutineItems != null)
//        {
//            foreach (IList<object> row in persistedRoutineItems)
//            {
//                int minRestTimeInSeconds = 0;
//                int maxRestTimeInSeconds = 0;

//                string restTimeInSeconds = row[4].ToString()!;
//                if (restTimeInSeconds.Contains('-'))
//                {
//                    string[] restTimeSegments = restTimeInSeconds.Split('-');
//                    minRestTimeInSeconds = Convert.ToInt32(restTimeSegments[0]);
//                    maxRestTimeInSeconds = Convert.ToInt32(restTimeSegments[1]);
//                }
//                else
//                {
//                    minRestTimeInSeconds = Convert.ToInt32(restTimeInSeconds);
//                    maxRestTimeInSeconds = minRestTimeInSeconds;
//                }

//                routineItems.Add(new RoutineItemDALModel()
//                {
//                    RoutineId = Convert.ToInt32(row[0]),
//                    RoutineName = row[1].ToString() ?? string.Empty,
//                    DayOfWeek = Convert.ToInt32(row[2]),
//                    Name = row[3].ToString() ?? string.Empty,
//                    RestTimeInSeconds = (minRestTimeInSeconds, maxRestTimeInSeconds),
//                    IsAnAlternatingSerie = (row[5].ToString() ?? string.Empty).Equals(YES.ToLower(), StringComparison.CurrentCultureIgnoreCase),
//                    IsActive = (row[6].ToString() ?? string.Empty).Equals(YES.ToLower(), StringComparison.CurrentCultureIgnoreCase)
//                });
//            }
//        }

//        return routineItems.GroupBy(r => r.RoutineId);
//    }
//}
