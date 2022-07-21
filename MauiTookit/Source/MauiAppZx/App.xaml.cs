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
        //var baseWindow = base.CreateWindow(activationState) 
        if (Windows is not null && Windows.Count > 0)
        {
            var mainWindow = Windows.First();
            mainWindow.Page = MainPage;
            return mainWindow;
        }

        var window = new ClassicalWindow()
        {
            Width = 800d,
            Height = 600d,
            IsShowFllowMouse = true,
            Title = PlatformShared.GetApplicationName(),
            WindowPresenterKind = Maui.Toolkitx.Options.WindowPresenterKind.Maximize,
#if MACCATALYST
            BackdropsKind = Maui.Toolkitx.Options.BackdropsKind.BlurEffect,
            BackdropConfigurations = new Maui.Toolkitx.Config.BackdropConfigurations
            {
                IsHighContrast = false,
                IsUseBaseKind = true,
                LuminosityOpacity = 0.9f,
                TintOpacity = 0.5f
            },
#elif WINDOWS
            BackdropsKind = Maui.Toolkitx.Options.BackdropsKind.Mica,
#endif
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
            options.CaptionHeight = 40d;
            options.CaptionActiveBackgroundColor = Colors.Transparent;
            options.WindowTitleBarKind = Maui.Toolkitx.Options.WindowTitleBarKind.Default;
            options.WindowButtonKind = Maui.Toolkitx.Options.WindowButtonKind.Show;
        }).UseShellView(options => 
        {
            options.IsSearchBarVisible = true;
            options.IsSettingVisible = true;
            options.IsPaneToggleButtonVisible = true;
            options.SettingConfigurations.Height = 35;
            //options.Background = Colors.Red;
            //options.ContentBackground = Colors.Blue;
        });
        
        return window;
    }
}
