using Maui.Toolkitx.Assists.Compositions;

namespace Maui.Toolkitx.Assists;
public partial class VisualElementAssists
{
    public static readonly BindableProperty AcrylicBrushProperty =
                           BindableProperty.CreateAttached("AcrylicBrush", typeof(MauiAcrylicBrush), typeof(VisualElementAssists), default, propertyChanged: AcrylicBrushPropertyChanged);

    public static MauiAcrylicBrush GetAcrylicBrush(VisualElement target) => (MauiAcrylicBrush)target.GetValue(AcrylicBrushProperty);
    public static void SetAcrylicBrush(VisualElement target, MauiAcrylicBrush value) => target.SetValue(AcrylicBrushProperty, value);

    private static void AcrylicBrushPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is null)
            return;

        if (oldValue is IAttachedObject attachedObject)
            attachedObject.Detach();

        if (newValue is IAttachedObject attachedObject1)
            attachedObject1.Attach(bindable);
    }
}
