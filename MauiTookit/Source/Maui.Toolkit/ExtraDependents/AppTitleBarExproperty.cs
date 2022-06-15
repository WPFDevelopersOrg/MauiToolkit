namespace Maui.Toolkit.ExtraDependents;

internal enum BindableActions
{
    Add,
    Update,
    Remove,
}

internal class BindableObjectEvenArgs : EventArgs
{
    public BindableActions Actions { get; set; }
    public BindableObject? Object { get; set; }
}


public class AppTitleBarExProperty
{
    private static Dictionary<BindableObject, BindableObject> __mapRects = new();

    public static readonly BindableProperty IsCanHitVisibleInTitleBarProperty =
                           BindableProperty.CreateAttached("IsCanHitVisibleInTitleBar", typeof(bool), typeof(AppTitleBarExProperty), false, propertyChanged: IsCanHitVisibleInTitleBarPropertyChanged);


    internal static EventHandler<BindableObjectEvenArgs>? BindiableObjectChangedEvent = default;

    public static bool GetIsCanHitVisibleInTitleBar(BindableObject target) => (bool)target.GetValue(IsCanHitVisibleInTitleBarProperty);
    public static void SetIsCanHitVisibleInTitleBar(BindableObject target, bool value) => target.SetValue(IsCanHitVisibleInTitleBarProperty, value);

    private static void IsCanHitVisibleInTitleBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable == null) return;
        if (newValue == null) return;

        if (!bool.TryParse(newValue?.ToString(), out var bResult))
            return;

        BindableObjectEvenArgs args = new()
        {
            Object = bindable,
        };
 
        if (!bResult)
        {
            if (!__mapRects.Remove(bindable, out var valueRect))
                return;

            args.Actions = BindableActions.Remove;
        }
        else
        {
            __mapRects[bindable] = bindable;
            args.Actions = BindableActions.Add;
        }

        BindiableObjectChangedEvent?.Invoke(bindable, args);
    }

    public static BindableObject[]? GetBindableObject()
    {
        if (__mapRects is null)
            return default;

        if (__mapRects.Count <= 0)
            return default;

        return __mapRects.Select(kv => kv.Key).ToArray();
    }

}
