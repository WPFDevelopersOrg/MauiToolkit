using System.Runtime.CompilerServices;

namespace Maui.Toolkitx;

public partial class WindowChrome : BindableObject
{
    public static readonly BindableProperty TitleFontSizeProperty =
                           BindableProperty.Create(propertyName: nameof(TitleFontSize),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: 16d,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty IconProperty =
                           BindableProperty.Create(propertyName: nameof(Icon),
                                                   returnType: typeof(string),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty CaptionHeightProperty =
                           BindableProperty.Create(propertyName: nameof(CaptionHeight),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: 40d,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowTitleBarKindProperty =
                           BindableProperty.Create(propertyName: nameof(WindowTitleBarKind),
                                                   returnType: typeof(WindowTitleBarKind),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowButtonKindProperty =
                           BindableProperty.Create(propertyName: nameof(WindowButtonKind),
                                                   returnType: typeof(WindowButtonKind),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty CaptionActiveBackgroundColorProperty =
                           BindableProperty.Create(propertyName: nameof(CaptionActiveBackgroundColor),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty CaptionInactiveBackgroundColorProperty =
                           BindableProperty.Create(propertyName: nameof(CaptionInactiveBackgroundColor),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty CaptionActiveForegroundColorProperty =
                           BindableProperty.Create(propertyName: nameof(CaptionActiveForegroundColor),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty CaptionInactiveForegroundColorProperty =
                           BindableProperty.Create(propertyName: nameof(CaptionInactiveForegroundColor),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);



    public double TitleFontSize
    {
        get => (double)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }

    public string? Icon
    {
        get => (string?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

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

    public WindowButtonKind WindowButtonKind
    {
        get => (WindowButtonKind)GetValue(WindowButtonKindProperty);
        set => SetValue(WindowButtonKindProperty, value);
    }

    public Color? CaptionActiveBackgroundColor
    {
        get => (Color?)GetValue(CaptionActiveBackgroundColorProperty);
        set => SetValue(CaptionActiveBackgroundColorProperty, value);
    }

    public Color? CaptionInactiveBackgroundColor
    {
        get => (Color?)GetValue(CaptionInactiveBackgroundColorProperty);
        set => SetValue(CaptionInactiveBackgroundColorProperty, value);
    }

    public Color? CaptionActiveForegroundColor
    {
        get => (Color?)GetValue(CaptionActiveForegroundColorProperty);
        set => SetValue(CaptionActiveForegroundColorProperty, value);
    }

    public Color? CaptionInactiveForegroundColor
    {
        get => (Color?)GetValue(CaptionInactiveForegroundColorProperty);
        set => SetValue(CaptionInactiveForegroundColorProperty, value);
    }



    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = default)
    {
        base.OnPropertyChanged(propertyName);
    }

    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }

}
