using Maui.Toolkitx.Platforms.Windows.Permanents;
using WinRT;
using MicrosoftBackdrops = Microsoft.UI.Composition.SystemBackdrops;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using Microsoftui = Microsoft.UI;
using MicrosoftuiComposition = Microsoft.UI.Composition;
using Maui.Toolkitx.Config;
using Maui.Toolkitx.Platforms.Windows.Extensions;

namespace Maui.Toolkitx.Platforms.Windows.Controllers;

internal class WinuiMicaController : IService
{
    public WinuiMicaController(Window window, BackdropConfigurations config)
    {
        _Window = window.Handler.PlatformView as MicrosoftuiXaml.Window;
        SystemDispatcherQueue.Instance.EnsureWindowsSystemDispatcherQueueController();

        _BackdropConfigurations = config;
    }

    readonly BackdropConfigurations _BackdropConfigurations;

    bool _IsStart = false;
    MicrosoftuiXaml.Window? _Window;

    MicrosoftBackdrops.MicaController? _MicaController;
    MicrosoftBackdrops.SystemBackdropConfiguration? _SystemBackdropConfiguration;

    bool IService.Run()
    {
        if (_IsStart)
            return true;

        if (!MicrosoftBackdrops.MicaController.IsSupported())
            return false;

        if (_Window is null)
            return false;

        _SystemBackdropConfiguration = new()
        {
            IsInputActive = true,
            IsHighContrast = _BackdropConfigurations.IsHighContrast,
            HighContrastBackgroundColor = _BackdropConfigurations.HighContrastBackgroundColor.ToPlatformColor(),
        };
        _Window.Activated += Window_Activated;
        _BackdropConfigurations.PropertyChanged += BackdropConfigurations_PropertyChanged;
        if (_Window.Content is MicrosoftuiXaml.FrameworkElement frameworkElement)
            frameworkElement.ActualThemeChanged += FrameworkElement_ActualThemeChanged;

        _MicaController = new()
        {
            Kind = _BackdropConfigurations.IsUseBaseKind ? MicrosoftBackdrops.MicaKind.Base : MicrosoftBackdrops.MicaKind.BaseAlt,
            LuminosityOpacity = _BackdropConfigurations.LuminosityOpacity,
            TintOpacity = _BackdropConfigurations.TintOpacity,
            TintColor = _BackdropConfigurations.TintColor.ToPlatformColor(),
        };
        LoadTheme();

        var iCompositionSupportsSystemBackdrop = _Window.As<MicrosoftuiComposition.ICompositionSupportsSystemBackdrop>();
        _MicaController.AddSystemBackdropTarget(iCompositionSupportsSystemBackdrop);
        _MicaController.SetSystemBackdropConfiguration(_SystemBackdropConfiguration);

        _IsStart = true;

        return true;
    }

    bool IService.Stop()
    {
        if (_IsStart)
        {
            _MicaController?.Dispose();

            if (_Window is not null)
            {
                _Window.Activated -= Window_Activated;
                _BackdropConfigurations.PropertyChanged -= BackdropConfigurations_PropertyChanged;
                if (_Window.Content is MicrosoftuiXaml.FrameworkElement frameworkElement)
                    frameworkElement.ActualThemeChanged -= FrameworkElement_ActualThemeChanged;
            }
        }

        _Window = default;
        _MicaController = default;
        _SystemBackdropConfiguration = default;
        _IsStart = false;
        return true;
    }

    bool LoadTheme()
    {
        if (_SystemBackdropConfiguration is null)
            return false;

        if (_Window?.Content is not MicrosoftuiXaml.FrameworkElement frameworkElement)
            return false;

        var theme = frameworkElement.ActualTheme;
        switch (theme)
        {
            case MicrosoftuiXaml.ElementTheme.Default:
                _SystemBackdropConfiguration.Theme = MicrosoftBackdrops.SystemBackdropTheme.Default;
                break;
            case MicrosoftuiXaml.ElementTheme.Light:
                _SystemBackdropConfiguration.Theme = MicrosoftBackdrops.SystemBackdropTheme.Light;
                break;
            case MicrosoftuiXaml.ElementTheme.Dark:
                _SystemBackdropConfiguration.Theme = MicrosoftBackdrops.SystemBackdropTheme.Dark;
                break;
            default:
                break;
        }

        return true;
    }

    private void Window_Activated(object sender, MicrosoftuiXaml.WindowActivatedEventArgs args)
    {
        if (_SystemBackdropConfiguration is null)
            return;

        _SystemBackdropConfiguration.IsInputActive = args.WindowActivationState != MicrosoftuiXaml.WindowActivationState.Deactivated;
    }

    private void FrameworkElement_ActualThemeChanged(MicrosoftuiXaml.FrameworkElement sender, object args) => LoadTheme();

    private void BackdropConfigurations_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_SystemBackdropConfiguration is null)
            return;

        if (_MicaController is null)
            return;

        _SystemBackdropConfiguration.IsHighContrast = _BackdropConfigurations.IsHighContrast;
        _SystemBackdropConfiguration.HighContrastBackgroundColor = _BackdropConfigurations.HighContrastBackgroundColor.ToPlatformColor();

        _MicaController.Kind = _BackdropConfigurations.IsUseBaseKind ? MicrosoftBackdrops.MicaKind.Base : MicrosoftBackdrops.MicaKind.BaseAlt;
        _MicaController.LuminosityOpacity = _BackdropConfigurations.LuminosityOpacity;
        _MicaController.TintOpacity = _BackdropConfigurations.TintOpacity;
        _MicaController.TintColor = _BackdropConfigurations.TintColor.ToPlatformColor();
    }
}
