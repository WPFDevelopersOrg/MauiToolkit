using Maui.Toolkitx;


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
        var window = base.CreateWindow(activationState);
        
        window.UseWindowChrome(options => 
        {
            options.BackdropsKind = Maui.Toolkitx.Options.BackdropsKind.Acrylic;
            options.ShowInSwitcher = true;
            options.TopMost = true;
            options.WindowAlignment = Maui.Toolkitx.Options.WindowAlignment.Center;
            options.Width = 1920d;
            options.Height = 1080d;
            options.WindowPresenterKind = Maui.Toolkitx.Options.WindowPresenterKind.Default;
            options.WindowTitleBarKind = Maui.Toolkitx.Options.WindowTitleBarKind.CustomTitleBarAndExtension;
        });

        return window;
    }
}
