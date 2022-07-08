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
            Width = 800d,
            Height = 600d,
            Title = PlatformShared.GetApplicationName(),
            BackdropsKind = Maui.Toolkitx.Options.BackdropsKind.Mica,
            Page = MainPage,
        }.UseWindowChrome(options => 
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
        }).UseShellView(options => 
        {
            options.IsSearchBarVisible = true;
            options.IsSettingVisible = true;
            options.IsPaneToggleButtonVisible = true;
            //options.Background = Colors.Red;
            //options.ContentBackground = Colors.Blue;
        });

        return window;
    }
}
