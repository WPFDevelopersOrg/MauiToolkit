using Maui.Toolkitx;
using Maui.Toolkitx.Controls;

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
        var window = new ClassicalWindow();
        window.Page = MainPage;
        window.UseWindowChrome(options => 
        {
            options.WindowTitleBarKind = Maui.Toolkitx.Options.WindowTitleBarKind.CustomTitleBarAndExtension;
        });

        return window;
    }
}
