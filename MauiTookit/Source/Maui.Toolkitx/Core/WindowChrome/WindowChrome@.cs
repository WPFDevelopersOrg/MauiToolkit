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

    public static readonly BindableProperty ShowInSwitcherProperty =
                           BindableProperty.Create(propertyName: nameof(ShowInSwitcher),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: true,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowAlignmentProperty =
                           BindableProperty.Create(propertyName: nameof(WindowAlignment),
                                                   returnType: typeof(WindowAlignment),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WidthProperty =
                           BindableProperty.Create(propertyName: nameof(Width),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: 800d,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty HeightProperty =
                           BindableProperty.Create(propertyName: nameof(Height),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: 500d,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty TopMostProperty =
                           BindableProperty.Create(propertyName: nameof(TopMost),
                                                   returnType: typeof(bool),
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

    public bool ShowInSwitcher
    {
        get => (bool)GetValue(ShowInSwitcherProperty);
        set => SetValue(ShowInSwitcherProperty, value);
    }

    public WindowAlignment WindowAlignment
    {
        get => (WindowAlignment)GetValue(WindowAlignmentProperty);
        set => SetValue(WindowAlignmentProperty, value);
    }

    public double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    public double Height
    {
        get => (double)GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    public bool TopMost
    {
        get => (bool)GetValue(TopMostProperty);
        set => SetValue(TopMostProperty, value);
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
