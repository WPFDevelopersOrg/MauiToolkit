using MicrosoftBackdrops = Microsoft.UI.Composition.SystemBackdrops;
using Microsoftui = Microsoft.UI.Xaml;
using MicrosoftuiComposition = Microsoft.UI.Composition;

namespace Maui.Toolkit.Platforms.Windows.Controllers;

internal class WinuiMicaController : IWinuiController
{
    public WinuiMicaController(Microsoftui.Window window)
    {
        _Window = window;
    }

    bool _IsStart = false;
    Microsoftui.Window? _Window;

    MicrosoftBackdrops.MicaController? _MicaController;
    //MicrosoftBackdrops.SystemBackdropConfiguration? _SystemBackdropConfiguration;

    bool IWinuiController.Run()
    {
        if (_IsStart)
            return true;

        if (!MicrosoftBackdrops.MicaController.IsSupported())
            return false;

        if (_Window is null)
            return false;

        //_SystemBackdropConfiguration = new();
        _Window.Activated += Window_Activated;
        if (_Window.Content is Microsoftui.FrameworkElement frameworkElement)
            frameworkElement.ActualThemeChanged += FrameworkElement_ActualThemeChanged;

        _MicaController = new();


        //_SystemBackdropConfiguration.IsInputActive = true;
        LoadTheme();

        // _MicaController.AddSystemBackdropTarget(_Window.As<MicrosoftuiComposition.ICompositionSupportsSystemBackdrop>())
        //micaController.SetSystemBackdropConfiguration(_SystemBackdropConfiguration);


        _IsStart = true;

        return true;
    }


    bool IWinuiController.Stop()
    {
        if (_IsStart)
        {
            if (_Window is not null)
            {
                _Window.Activated -= Window_Activated;
                if (_Window.Content is Microsoftui.FrameworkElement frameworkElement)
                    frameworkElement.ActualThemeChanged -= FrameworkElement_ActualThemeChanged;
            }
        }

        _Window = default;
        _IsStart = false;
        return true;
    }

    private void Window_Activated(object sender, Microsoftui.WindowActivatedEventArgs args)
    {
         
    }

    private void FrameworkElement_ActualThemeChanged(Microsoftui.FrameworkElement sender, object args) => LoadTheme();

    bool LoadTheme()
    {
        if (_Window?.Content is not Microsoftui.FrameworkElement frameworkElement)
            return false;

        var theme = frameworkElement.ActualTheme;
        switch (theme)
        {
            case Microsoftui.ElementTheme.Default:
                //_SystemBackdropConfiguration.Theme = MicrosoftuiComposition.SystemBackdrops.SystemBackdropTheme.Default;
                break;
            case Microsoftui.ElementTheme.Light:
                //_SystemBackdropConfiguration.Theme = MicrosoftuiComposition.SystemBackdrops.SystemBackdropTheme.Light;
                break;
            case Microsoftui.ElementTheme.Dark:
                //_SystemBackdropConfiguration.Theme = MicrosoftuiComposition.SystemBackdrops.SystemBackdropTheme.Dark;
                break;
            default:
                break;
        }

        return true;
    }

}
