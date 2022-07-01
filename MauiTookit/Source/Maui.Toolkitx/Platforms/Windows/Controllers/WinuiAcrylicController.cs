﻿using Maui.Toolkitx.Platforms.Windows.Permanents;
using WinRT;
using MicrosoftBackdrops = Microsoft.UI.Composition.SystemBackdrops;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using MicrosoftuiComposition = Microsoft.UI.Composition;
using Microsoftui = Microsoft.UI;

namespace Maui.Toolkitx.Platforms.Windows.Controllers;

internal class WinuiAcrylicController : IService
{
    public WinuiAcrylicController(Window window)
    {
        _Window = window.Handler.PlatformView as MicrosoftuiXaml.Window;
        SystemDispatcherQueue.Instance.EnsureWindowsSystemDispatcherQueueController();
    }

    bool _IsStart = false;
    MicrosoftuiXaml.Window? _Window;
    MicrosoftBackdrops.DesktopAcrylicController? _AcrylicController;
    MicrosoftBackdrops.SystemBackdropConfiguration? _SystemBackdropConfiguration;

    bool IService.Run()
    {
        if (_IsStart)
            return true;

        if (!MicrosoftBackdrops.DesktopAcrylicController.IsSupported())
            return false;

        if (_Window is null)
            return false;

        _SystemBackdropConfiguration = new()
        {
            IsInputActive = true,
            IsHighContrast = false,
            HighContrastBackgroundColor = Microsoftui.Colors.DarkSlateGray,
        };

        _Window.Activated += Window_Activated;
        if (_Window.Content is MicrosoftuiXaml.FrameworkElement frameworkElement)
            frameworkElement.ActualThemeChanged += FrameworkElement_ActualThemeChanged;

        _AcrylicController = new()
        {
            LuminosityOpacity =0.1f,
            TintOpacity = 0.2f,
            TintColor = Microsoftui.Colors.BlueViolet,
        };

        LoadTheme();

        var iCompositionSupportsSystemBackdrop = _Window.As<MicrosoftuiComposition.ICompositionSupportsSystemBackdrop>();
        _AcrylicController.AddSystemBackdropTarget(iCompositionSupportsSystemBackdrop);
        _AcrylicController.SetSystemBackdropConfiguration(_SystemBackdropConfiguration);

        _IsStart = true;
        return true;
    }

    bool IService.Stop()
    {
        if (_IsStart)
        {
            _AcrylicController?.Dispose();

            if (_Window is not null)
            {
                _Window.Activated -= Window_Activated;
                if (_Window.Content is MicrosoftuiXaml.FrameworkElement frameworkElement)
                    frameworkElement.ActualThemeChanged -= FrameworkElement_ActualThemeChanged;
            }
        }

        _Window = default;
        _AcrylicController = default;
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
}
