using Maui.Toolkitx;
using Maui.Toolkitx.Extensions;
using Maui.Toolkitx.Shared;

namespace MauiAppZx;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiToolkitx()
            .UseStatusBar(options =>
            {
#if MACCATALYST
                options.Icon1 = PlatformShared.CreatePathBuilder()
                                              .AddArgument("Resources")
                                              .AddArgument("app.png")
                                              .Build();
#endif
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
