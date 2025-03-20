// DEPRECATED, KEEPED FOR REFERENCE

//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Services;
//using Google.Apis.Sheets.v4;
//using Google.Apis.Sheets.v4.Data;
//using GymTron.Domain.Services;
//using GymTron.Insfrastructure;

//internal class GoogleSheetsService(IResourceProvider resourceProvider) : IGoogleSheetsService
//{


//    private readonly string[] SCOPES = [SheetsService.Scope.Spreadsheets];


//    private readonly IResourceProvider _resourceProvider = resourceProvider;


//    public IList<IList<object>> ReadFile(string range)
//    {
//        SheetsService googleService = BuildGoogleSheetsService();

//        SpreadsheetsResource.ValuesResource.GetRequest request = googleService.Spreadsheets.Values.Get(Consts.SPREADSHEET_ID, range);

//        ValueRange response = request.Execute();

//        return response.Values;
//    }


//    public void AddRowToNextAvailable(string sheetName, IList<object> newRow)
//    {
//        SheetsService googleService = BuildGoogleSheetsService();

//        SpreadsheetsResource.ValuesResource.GetRequest getRequest = googleService.Spreadsheets.Values.Get(Consts.SPREADSHEET_ID, $"{sheetName}");
//        ValueRange response = getRequest.Execute();

//        IList<IList<object>> values = response.Values;

//        int nextRow = values != null ? values.Count + 1 : 1;

//        string range = $"{sheetName}!A{nextRow}";

//        ValueRange valueRange = new()
//        {
//            Values = [newRow]
//        };

//        SpreadsheetsResource.ValuesResource.AppendRequest appendRequest = googleService.Spreadsheets.Values.Append(valueRange, Consts.SPREADSHEET_ID, range);
//        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

//        appendRequest.Execute();
//    }


//    public void AddRowsToNextAvailable(string sheetName, IList<IList<object>> newRows)
//    {
//        SheetsService googleService = BuildGoogleSheetsService();

//        SpreadsheetsResource.ValuesResource.GetRequest getRequest = googleService.Spreadsheets.Values.Get(Consts.SPREADSHEET_ID, sheetName);
//        ValueRange response = getRequest.Execute();

//        IList<IList<object>> values = response.Values;
//        int nextRow = values != null ? values.Count + 1 : 1;

//        string range = $"{sheetName}!A{nextRow}";

//        ValueRange valueRange = new()
//        {
//            Values = newRows
//        };

//        SpreadsheetsResource.ValuesResource.AppendRequest appendRequest = googleService.Spreadsheets.Values.Append(valueRange, Consts.SPREADSHEET_ID, range);
//        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

//        appendRequest.Execute();
//    }


//    public void UpdateRowById(string sheetName, string id, IList<object> updatedRow)
//    {
//        SheetsService googleService = BuildGoogleSheetsService();

//        SpreadsheetsResource.ValuesResource.GetRequest getRequest = googleService.Spreadsheets.Values.Get(Consts.SPREADSHEET_ID, sheetName);
//        ValueRange response = getRequest.Execute();

//        IList<IList<object>> values = response.Values;

//        if (values == null || values.Count == 0)
//            return;

//        int rowIndex = -1;
//        for (int i = 0; i < values.Count; i++)
//        {
//            IList<object> row = values[i];
//            if (row.Count > 0 && row[0].ToString() == id)
//            {
//                rowIndex = i + 1;
//                break;
//            }
//        }

//        if (rowIndex == -1)
//            return;

//        char lastColumn = (char)('A' + updatedRow.Count - 1);
//        string range = $"{sheetName}!A{rowIndex}:{lastColumn}{rowIndex}";

//        ValueRange valueRange = new()
//        {
//            Values = [updatedRow]
//        };

//        SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = googleService.Spreadsheets.Values.Update(valueRange, Consts.SPREADSHEET_ID, range);
//        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

//        updateRequest.Execute();
//    }


//    private SheetsService BuildGoogleSheetsService()
//    {
//        Stream stream = _resourceProvider.GetResourceStream("GymTron.App.Resources.Json.secrets_google_docs.json");

//        GoogleCredential credential;
//        credential = GoogleCredential.FromStream(stream).CreateScoped(SCOPES);

//        SheetsService service = new(new BaseClientService.Initializer()
//        {
//            HttpClientInitializer = credential,
//            ApplicationName = Consts.SPREADSHEET_APPLICATION_NAME,
//        });

//        return service;
//    }
//}