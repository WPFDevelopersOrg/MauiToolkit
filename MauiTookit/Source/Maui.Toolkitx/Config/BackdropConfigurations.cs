namespace Maui.Toolkitx.Config;
public class BackdropConfigurations : BindableObject
{
    public static readonly BindableProperty IsHighContrastProperty =
                           BindableProperty.Create(propertyName: nameof(IsHighContrast),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(BackdropConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty HighContrastBackgroundColorProperty =
                           BindableProperty.Create(propertyName: nameof(HighContrastBackgroundColor),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(BackdropConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty IsUseBaseKindProperty =
                           BindableProperty.Create(propertyName: nameof(IsUseBaseKind),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(BackdropConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty LuminosityOpacityProperty =
                           BindableProperty.Create(propertyName: nameof(LuminosityOpacity),
                                                   returnType: typeof(float),
                                                   declaringType: typeof(BackdropConfigurations),
                                                   defaultValue: 1f);

    public static readonly BindableProperty TintOpacityProperty =
                           BindableProperty.Create(propertyName: nameof(TintOpacity),
                                                   returnType: typeof(float),
                                                   declaringType: typeof(BackdropConfigurations),
                                                   defaultValue: 1f);

    public static readonly BindableProperty TintColorProperty =
                           BindableProperty.Create(propertyName: nameof(TintColor),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(BackdropConfigurations),
                                                   defaultValue: Colors.Transparent);

    
    public bool IsHighContrast
    {
        get => (bool)GetValue(IsHighContrastProperty);
        set => SetValue(IsHighContrastProperty, value);
    }

    public Color? HighContrastBackgroundColor
    {
        get => (Color?)GetValue(HighContrastBackgroundColorProperty);
        set => SetValue(HighContrastBackgroundColorProperty, value);
    }

    public bool IsUseBaseKind
    {
        get => (bool)GetValue(IsUseBaseKindProperty);
        set => SetValue(IsUseBaseKindProperty, value);
    }

    public float LuminosityOpacity
    {
        get => (float)GetValue(LuminosityOpacityProperty);
        set => SetValue(LuminosityOpacityProperty, value);
    }

    public float TintOpacity
    {
        get => (float)GetValue(TintOpacityProperty);
        set => SetValue(TintOpacityProperty, value);
    }

    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

}
