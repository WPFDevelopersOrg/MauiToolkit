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
        return true;
    }

    bool IService.Stop()
    {
        return true;
    }
}
