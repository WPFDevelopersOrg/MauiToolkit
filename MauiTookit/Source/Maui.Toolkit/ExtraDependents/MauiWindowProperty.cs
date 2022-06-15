using Maui.Toolkit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Toolkit.ExtraDependents;
public class MauiWindowProperty
{
    public static readonly BindableProperty WindowBackdropsKindProperty =
                       BindableProperty.CreateAttached("WindowBackdropsKind", typeof(BackdropsKind), typeof(AppTitleBarExProperty), false, propertyChanged: WindowBackdropsKindPropertyChanged);

    public static BackdropsKind GetWindowBackdropsKind(BindableObject target) => (BackdropsKind)target.GetValue(WindowBackdropsKindProperty);
    public static void SetWindowBackdropsKind(BindableObject target, BackdropsKind value) => target.SetValue(WindowBackdropsKindProperty, value);

    private static void WindowBackdropsKindPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable == null) return;
        if (newValue == null) return;

        if (bindable is not Window window)
            return;

        if (!Enum.TryParse(newValue?.ToString(), out BackdropsKind kind))
            return;

        switch (kind)
        {
            case BackdropsKind.Default:
                 
                break;
            case BackdropsKind.Mica:

                break;
            case BackdropsKind.Acrylic:

                break;
            default:
                break;
        }


    }

}
