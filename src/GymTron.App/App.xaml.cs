using GymTron.App.Services;
using GymTron.Domain.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;

namespace GymTron.App;

public partial class App : Microsoft.Maui.Controls.Application
{


    public static IServiceProvider Services { get; private set; } = null!;
    private readonly IExceptionLogger<App> _logger;


    public App(IServiceProvider serviceProvider, IExceptionLogger<App> logger)
    {
        InitializeComponent();

        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-ES");

        Services = serviceProvider;
        _logger = logger;

        ITrainingService trainingService = serviceProvider.GetService<ITrainingService>()!;

        MainPage = new AppShell(trainingService);
    }


    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception");
            HandleException(ex, "Unhandled Exception");
        }
    }


    private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        _logger.LogError(e.Exception, "Unobserved Task Exception");
        HandleException(e.Exception, "Unobserved Task Exception");
        e.SetObserved();
    }


    private void HandleException(Exception ex, string source)
    {
        Debug.WriteLine($"[{source}] {ex}");

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.DisplayAlert("Error", "S'ha produït un error inesperat.", "D'acord");
        });
    }
}
