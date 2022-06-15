using Maui.Toolkit.Core;
using Maui.Toolkit.Platforms.Windows.Helpers;
using WinRT;
using MicrosoftBackdrops = Microsoft.UI.Composition.SystemBackdrops;
using Microsoftui = Microsoft.UI.Xaml;
using MicrosoftuiComposition = Microsoft.UI.Composition;

namespace Maui.Toolkit.Platforms.Windows.Controllers;

internal class WinuiMicaController : IWindowController
{
    public WinuiMicaController(Microsoftui.Window window)
    {
        _Window = window;
        CoreMessageHelper.Instance.EnsureWindowsSystemDispatcherQueueController();
    }

    bool _IsStart = false;
    Microsoftui.Window? _Window;

    MicrosoftBackdrops.MicaController? _MicaController;
    MicrosoftBackdrops.SystemBackdropConfiguration? _SystemBackdropConfiguration;

    bool IWindowController.Run()
    {
        if (_IsStart)
            return true;

        if (!MicrosoftBackdrops.MicaController.IsSupported())
            return false;

        if (_Window is null)
            return false;

        _SystemBackdropConfiguration = new();
        _Window.Activated += Window_Activated;
        if (_Window.Content is Microsoftui.FrameworkElement frameworkElement)
            frameworkElement.ActualThemeChanged += FrameworkElement_ActualThemeChanged;

        _SystemBackdropConfiguration.IsInputActive = true;
        _MicaController = new();

        LoadTheme();

        var iCompositionSupportsSystemBackdrop = _Window.As<MicrosoftuiComposition.ICompositionSupportsSystemBackdrop>();
        _MicaController.AddSystemBackdropTarget(iCompositionSupportsSystemBackdrop);
        _MicaController.SetSystemBackdropConfiguration(_SystemBackdropConfiguration);

        _IsStart = true;

        return true;
    }

    bool IWindowController.Stop()
    {
        if (_IsStart)
        {
            _MicaController?.Dispose();

            if (_Window is not null)
            {
                _Window.Activated -= Window_Activated;
                if (_Window.Content is Microsoftui.FrameworkElement frameworkElement)
                    frameworkElement.ActualThemeChanged -= FrameworkElement_ActualThemeChanged;
            }
        }

        _Window = default;
        _MicaController = default;
        _SystemBackdropConfiguration = default;
        _IsStart = false;
        return true;
    }

    private void Window_Activated(object sender, Microsoftui.WindowActivatedEventArgs args)
    {
        if (_SystemBackdropConfiguration is null)
            return;

        _SystemBackdropConfiguration.IsInputActive = args.WindowActivationState != Microsoftui.WindowActivationState.Deactivated;
    }

    private void FrameworkElement_ActualThemeChanged(Microsoftui.FrameworkElement sender, object args) => LoadTheme();

    bool LoadTheme()
    {
        if (_SystemBackdropConfiguration is null)
            return false;

        if (_Window?.Content is not Microsoftui.FrameworkElement frameworkElement)
            return false;

        var theme = frameworkElement.ActualTheme;
        switch (theme)
        {
            case Microsoftui.ElementTheme.Default:
                _SystemBackdropConfiguration.Theme = MicrosoftBackdrops.SystemBackdropTheme.Default;
                break;
            case Microsoftui.ElementTheme.Light:
                _SystemBackdropConfiguration.Theme = MicrosoftBackdrops.SystemBackdropTheme.Light;
                break;
            case Microsoftui.ElementTheme.Dark:
                _SystemBackdropConfiguration.Theme = MicrosoftBackdrops.SystemBackdropTheme.Dark;
                break;
            default:
                break;
        }

        return true;
    }

}
