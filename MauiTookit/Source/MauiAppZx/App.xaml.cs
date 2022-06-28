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
            options.BackdropsKind = Maui.Toolkitx.Options.BackdropsKind.Mica;
        });

        return window;
    }
}
