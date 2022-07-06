using Maui.Toolkitx.Options;
using System.Runtime.CompilerServices;

namespace Maui.Toolkitx;

public partial class WindowChrome
{
    public static readonly BindableProperty IsObjectCanHitProperty =
                           BindableProperty.CreateAttached("IsObjectCanHit", typeof(WindowChrome), typeof(WindowChrome), default, propertyChanged: IsObjectCanHitPropertyChanged);


    public static WindowChrome GetIsObjectCanHit(BindableObject target) => (WindowChrome)target.GetValue(IsObjectCanHitProperty);
    public static void SetIsObjectCanHit(BindableObject target, WindowChrome value) => target.SetValue(IsObjectCanHitProperty, value);


    private static void IsObjectCanHitPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        
    }

}
