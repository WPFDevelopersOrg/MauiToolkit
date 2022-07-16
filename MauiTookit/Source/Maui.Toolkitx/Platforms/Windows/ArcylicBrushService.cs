using Maui.Toolkitx.Assists.Compositions;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Media;
using PlatformControls = Microsoft.UI.Xaml.Controls;
using PlatformSharps = Microsoft.UI.Xaml.Shapes;
using PlatformXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkitx;

internal class ArcylicBrushService : IArcylicBrushService
{
    public ArcylicBrushService(VisualElement visualElement, MauiAcrylicBrush mauiAcrylicBrush)
    {
        ArgumentNullException.ThrowIfNull(visualElement);
        ArgumentNullException.ThrowIfNull(mauiAcrylicBrush);
        _VisualElement = visualElement;
        _AcrylicBrush = mauiAcrylicBrush;
    }

    readonly VisualElement _VisualElement;
    readonly MauiAcrylicBrush _AcrylicBrush;

    AcrylicBrush? _PlatformAcrylicBrush;

    bool IService.Run()
    {
        LoadEvent();
        LoadAcrylicBrush();
        _VisualElement.HandlerChanged += VisualElement_HandlerChanged;
        _AcrylicBrush.PropertyChanged += AcrylicBrush_PropertyChanged;
        return true;
    }

    bool IService.Stop()
    {
        UnloadEvent();
        _VisualElement.HandlerChanged -= VisualElement_HandlerChanged;
        _AcrylicBrush.PropertyChanged -= AcrylicBrush_PropertyChanged;
        return true;
    }

    bool LoadAcrylicBrush()
    {
        if (_VisualElement.Handler is null)
            return false;

        if (_VisualElement.Handler.PlatformView is null)
            return false;

        var platformView = _VisualElement.Handler.PlatformView;

        _PlatformAcrylicBrush ??= new()
        {
            TintColor = _AcrylicBrush.TintColor.ToWindowsColor(),
            TintLuminosityOpacity = _AcrylicBrush.TintLuminosityOpacity,
            TintOpacity = _AcrylicBrush.TintOpacity,
            FallbackColor = _AcrylicBrush.FallbackColor.ToWindowsColor(),
        };

        if (platformView is PlatformSharps.Shape shape)
            shape.Fill = _PlatformAcrylicBrush;
        else if (platformView is PlatformControls.Panel panel)
            panel.Background = _PlatformAcrylicBrush;
        else if (platformView is PlatformControls.Control control)
            control.Background = _PlatformAcrylicBrush;

        return true;
    }

    bool LoadEvent()
    {
        if (_VisualElement.Handler is null)
            return false;

        if (_VisualElement.Handler.PlatformView is null)
            return false;

        var platformView = _VisualElement.Handler.PlatformView;
        if (platformView is PlatformXaml.FrameworkElement framworkElement)
            framworkElement.Loaded += FramworkElement_Loaded;

        return true;
    }

    bool UnloadEvent()
    {
        if (_VisualElement.Handler is null)
            return false;

        if (_VisualElement.Handler.PlatformView is null)
            return false;

        var platformView = _VisualElement.Handler.PlatformView;
        if (platformView is PlatformXaml.FrameworkElement framworkElement)
            framworkElement.Loaded -= FramworkElement_Loaded;

        return true;
    }

    private void VisualElement_HandlerChanged(object? sender, EventArgs e) => LoadEvent();

    private void AcrylicBrush_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_PlatformAcrylicBrush is null)
            return;

        switch (e.PropertyName)
        {
            case nameof(MauiAcrylicBrush.TintColor):
                _PlatformAcrylicBrush.TintColor = _AcrylicBrush.TintColor.ToWindowsColor();
                break;
            case nameof(MauiAcrylicBrush.TintOpacity):
                _PlatformAcrylicBrush.TintOpacity = _AcrylicBrush.TintOpacity;
                break;
            case nameof(MauiAcrylicBrush.TintLuminosityOpacity):
                _PlatformAcrylicBrush.TintLuminosityOpacity = _AcrylicBrush.TintLuminosityOpacity;
                break;
            case nameof(MauiAcrylicBrush.FallbackColor):
                _PlatformAcrylicBrush.FallbackColor = _AcrylicBrush.FallbackColor.ToWindowsColor();
                break;
            default:
                break;
        }
    }

    private void FramworkElement_Loaded(object sender, PlatformXaml.RoutedEventArgs e) => LoadAcrylicBrush();

}
