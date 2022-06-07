using Maui.Toolkit;
using Maui.Toolkit.Shared;

namespace MauiAppx;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseWindowStartup(options => 
            {
                options.PresenterKind = WindowPresenterKind.Default;
            })
            .UseStatusBar(options => 
            {

#if WINDOWS

                options.IconFilePath = PlatformShared.CreatePathBuilder()
                                                     .AddArgument("appicon.ico")
                                                     .Build();

#elif MACCATALYST

                options.IconFilePath = PlatformShared.CreatePathBuilder()
                                                     .AddArgument("Resources")
                                                     .AddArgument("application.png")
                                                     .Build();
#endif

            })
            .UseMessageNotify()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
