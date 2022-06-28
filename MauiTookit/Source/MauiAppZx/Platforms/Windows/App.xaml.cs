using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiAppZx.WinUI;
/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    static Mutex? __SingleMutex;

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        if (!IsSingleInstance())
        {
            //Process.GetCurrentProcess().Kill();
            Environment.Exit(0);
            return;
        }

        base.OnLaunched(args);
    }

    protected override bool IsOverridableInterface(Guid iid)
    {
        return base.IsOverridableInterface(iid);
    }

    static bool IsSingleInstance()
    {
        const string applicationId = "813342EB-7796-4B13-98F1-14C99E778C6E";
        __SingleMutex = new Mutex(false, applicationId);
        GC.KeepAlive(__SingleMutex);

        try
        {
            return __SingleMutex.WaitOne(0, false);
        }
        catch (Exception)
        {
            __SingleMutex.ReleaseMutex();
            return __SingleMutex.WaitOne(0, false);
        }

    }
}

