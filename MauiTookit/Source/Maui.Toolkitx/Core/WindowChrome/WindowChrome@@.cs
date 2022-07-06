using System.Runtime.CompilerServices;

namespace Maui.Toolkitx;

public partial class WindowChrome : BindableObject
{
    public static readonly BindableProperty CaptionHeightProperty =
                           BindableProperty.Create(propertyName: nameof(CaptionHeight),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowTitleBarKindProperty =
                           BindableProperty.Create(propertyName: nameof(WindowTitleBarKind),
                                                   returnType: typeof(WindowTitleBarKind),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);


    public double CaptionHeight
    {
        get => (double)GetValue(CaptionHeightProperty);
        set => SetValue(CaptionHeightProperty, value);
    }

    public WindowTitleBarKind WindowTitleBarKind
    {
        get => (WindowTitleBarKind)GetValue(WindowTitleBarKindProperty);
        set => SetValue(WindowTitleBarKindProperty, value);
    }


    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = default)
    {
        base.OnPropertyChanged(propertyName);
    }

    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(bindable);

    }

}
