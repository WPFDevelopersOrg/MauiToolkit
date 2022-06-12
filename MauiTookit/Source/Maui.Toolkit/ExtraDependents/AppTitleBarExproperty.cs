namespace Maui.Toolkit.ExtraDependents;

internal enum BindableActions
{
    Add,
    Remove,
}

internal class BindableObjectEvenArgs : EventArgs
{
    public BindableActions Actions { get; set; }
    public Rect RectValue { get; set; }
}


public class AppTitleBarExproperty
{
    private static Dictionary<BindableObject, Rect> __mapRects = new();

    public static readonly BindableProperty IsCanHitVisibleInTitleBarProperty =
                           BindableProperty.CreateAttached("IsCanHitVisibleInTitleBar", typeof(bool), typeof(AppTitleBarExproperty), false, propertyChanged: IsCanHitVisibleInTitleBarPropertyChanged);


    internal static EventHandler<BindableObjectEvenArgs?>? BindiableObjectChangedEvent;

    public static bool GetIsCanHitVisibleInTitleBar(BindableObject target) => (bool)target.GetValue(IsCanHitVisibleInTitleBarProperty);
    public static void SetIsCanHitVisibleInTitleBar(BindableObject target, bool value) => target.SetValue(IsCanHitVisibleInTitleBarProperty, value);

    private static void IsCanHitVisibleInTitleBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable == null) return;
        if (newValue == null) return;

        if (bindable is not VisualElement visualElement)
            return;

        if (!bool.TryParse(newValue?.ToString(), out var bResult))
            return;

        BindableObjectEvenArgs args = new();
        if (!bResult)
        {
            visualElement.Loaded -= VisualElement_Loaded;
            visualElement.SizeChanged -= VisualElement_SizeChanged;

            if (!__mapRects.Remove(bindable, out var valueRect))
                return;

            args.Actions = BindableActions.Remove;
            args.RectValue = valueRect;
        }
        else
        {
            visualElement.Loaded += VisualElement_Loaded;
            visualElement.SizeChanged += VisualElement_SizeChanged;

            args.Actions = BindableActions.Add;
            //args.RectValue = valueRect;
        }

        BindiableObjectChangedEvent?.Invoke(bindable, args);
    }

    private static void VisualElement_SizeChanged(object? sender, EventArgs e)
    {
        
    }

    private static void VisualElement_Loaded(object? sender, EventArgs e)
    {
        
    }
}
