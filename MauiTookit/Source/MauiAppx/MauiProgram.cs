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
                options.TitleBarKind = WindowTitleBarKind.ExtendsContentIntoTitleBar;
                options.PresenterKind = WindowPresenterKind.Default;
                options.TitleBarBackgroundColor = Colors.Transparent;
                options.TitleBarBackgroundInactiveColor = Colors.Transparent;
                options.BackdropsKind = BackdropsKind.Mica;
                //options.TitleBarForegroundColor = Colors.White;
                options.Size = new Size(1920, 1080);
            })
            .UseStatusBar(options => 
            {

#if WINDOWS

                options.IconFilePath = PlatformShared.CreatePathBuilder()
                                                     .AddArgument("Resources")
                                                     .AddArgument("AppIcon")
                                                     .AddArgument("application128.ico")
                                                     .Build();

#elif MACCATALYST

                options.IconFilePath = PlatformShared.CreatePathBuilder()
                                                     .AddArgument("Resources")
                                                     .AddArgument("app.png")
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
