using Maui.Toolkitx.Options;

namespace Maui.Toolkitx;

public partial class WindowChrome : BindableObject
{
    public static readonly BindableProperty CaptionHeightProperty = 
                           BindableProperty.Create(propertyName: nameof(CaptionHeight),
                                                   returnType:typeof(double),
                                                   declaringType:typeof(WindowChrome),
                                                   defaultValue:default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowPresenterKindProperty =
                           BindableProperty.Create(propertyName: nameof(WindowPresenterKind),
                                                   returnType: typeof(WindowPresenterKind),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowTitleBarKindProperty =
                           BindableProperty.Create(propertyName: nameof(WindowTitleBarKind),
                                                   returnType: typeof(WindowTitleBarKind),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty BackdropsKindProperty =
                           BindableProperty.Create(propertyName: nameof(BackdropsKind),
                                                   returnType: typeof(BackdropsKind),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);



    public double CaptionHeight
    {
        get => (double)GetValue(CaptionHeightProperty);
        set => SetValue(CaptionHeightProperty, value);
    }

    public WindowPresenterKind WindowPresenterKind
    {
        get => (WindowPresenterKind)GetValue(WindowPresenterKindProperty);
        set => SetValue(WindowPresenterKindProperty, value);
    }

    public WindowTitleBarKind WindowTitleBarKind
    {
        get => (WindowTitleBarKind)GetValue(WindowTitleBarKindProperty);
        set => SetValue(WindowTitleBarKindProperty, value);
    }

    public BackdropsKind BackdropsKind
    {
        get => (BackdropsKind)GetValue(BackdropsKindProperty);
        set => SetValue(BackdropsKindProperty, value);
    }

    private bool SetProperty()
    {
        return true;
    }


    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(bindable);
        
    }
}
