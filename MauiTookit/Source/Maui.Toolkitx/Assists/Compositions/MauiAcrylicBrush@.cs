using Maui.Toolkitx.Services;

namespace Maui.Toolkitx.Assists.Compositions;
public partial class MauiAcrylicBrush : IAttachedObject
{
    public MauiAcrylicBrush()
    {

    }

    IArcylicBrushService? _Service = default;

    bool _IsAttached = false;
    bool IAttachedObject.IsAttached => _IsAttached;

    VisualElement? _AssociatedObject = default;
    BindableObject? IAttachedObject.AssociatedObject => _AssociatedObject;

    void IAttachedObject.Attach(BindableObject bindableObject)
    {
        if (bindableObject is not VisualElement visualElement)
            return;

#if WINDOWS || MACCATALYST || IOS || ANDROID
        _Service = new ArcylicBrushService(visualElement, this);
        _Service.Run();
#endif
        _IsAttached = true;
        _AssociatedObject = visualElement;
    }

    void IAttachedObject.Detach()
    {
        _Service?.Stop();
        _Service = default;
        _IsAttached = false;
        _AssociatedObject = null;
    }
}
