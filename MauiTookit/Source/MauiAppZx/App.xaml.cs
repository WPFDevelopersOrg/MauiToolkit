using Maui.Toolkitx;
using Maui.Toolkitx.Controls;
using Maui.Toolkitx.Extensions;
using Maui.Toolkitx.Shared;

namespace MauiAppZx;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        //var window = base.CreateWindow(activationState);
        var window = new ClassicalWindow()
        {
            Title = PlatformShared.GetApplicationName(),
            BackdropsKind = Maui.Toolkitx.Options.BackdropsKind.Mica,
        };
        window.Page = MainPage;
        window.UseWindowChrome(options => 
        {
#if WINDOWS

                options.Icon = PlatformShared.CreatePathBuilder()
                                             .AddArgument("Resources")
                                             .AddArgument("AppIcon")
                                             .AddArgument("application128.ico")
                                             .Build();

#elif MACCATALYST

                options.Icon = PlatformShared.CreatePathBuilder()
                                             .AddArgument("Resources")
                                             .AddArgument("app.png")
                                             .Build();
#endif
            options.CaptionHeight = 45d;
            options.CaptionActiveBackgroundColor = Colors.Transparent;
            options.WindowTitleBarKind = Maui.Toolkitx.Options.WindowTitleBarKind.Default;
        });

        return window;
    }
}
