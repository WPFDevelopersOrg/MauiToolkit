using Maui.Toolkitx.Compositions;

namespace Maui.Toolkitx;

internal class ArcylicBrushService : IArcylicBrushService
{
    public ArcylicBrushService(VisualElement visualElement, MauiAcrylicBrush mauiAcrylicBrush)
    {
        ArgumentNullException.ThrowIfNull(visualElement);
        _VisualElement = visualElement;
        _AcrylicBrush = mauiAcrylicBrush;
    }

    readonly VisualElement _VisualElement;
    readonly MauiAcrylicBrush _AcrylicBrush;

    bool IService.Run()
    {
        LoadEvent();

        _VisualElement.HandlerChanged += VisualElement_HandlerChanged;
        _AcrylicBrush.PropertyChanged += AcrylicBrush_PropertyChanged;
        return true;
    }

    bool IService.Stop()
    {
        return true;
    }

    bool LoadEvent()
    {
        if (_VisualElement.Handler is null)
            return false;

        if (_VisualElement.Handler.PlatformView is null)
            return false;

        var platformView = _VisualElement.Handler.PlatformView;
        
        return true;
    }

    bool UnloadEvent()
    {
        if (_VisualElement.Handler is null)
            return false;

        if (_VisualElement.Handler.PlatformView is null)
            return false;

        var platformView = _VisualElement.Handler.PlatformView;
      

        return true;
    }

    private void AcrylicBrush_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        
    }

    private void VisualElement_HandlerChanged(object? sender, EventArgs e) => LoadEvent();

}
