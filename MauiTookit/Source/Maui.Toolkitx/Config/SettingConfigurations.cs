namespace Maui.Toolkitx.Config;

public class SettingConfigurations : BindableObject
{
    public static readonly BindableProperty TextProperty =
                           BindableProperty.Create(propertyName: nameof(Text),
                                                   returnType: typeof(string),
                                                   declaringType: typeof(SettingConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty HeightProperty =
                           BindableProperty.Create(propertyName: nameof(Height),
                                                   returnType: typeof(double?),
                                                   declaringType: typeof(SettingConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty WidthProperty =
                           BindableProperty.Create(propertyName: nameof(Width),
                                                   returnType: typeof(double?),
                                                   declaringType: typeof(SettingConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty MarginProperty =
                           BindableProperty.Create(propertyName: nameof(Margin),
                                                   returnType: typeof(Thickness?),
                                                   declaringType: typeof(SettingConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty SelectedProperty =
                           BindableProperty.Create(propertyName: nameof(Selected),
                                                   returnType: typeof(ICommand),
                                                   declaringType: typeof(SearchBarConfigurations),
                                                   defaultValue: default);

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public double? Width
    {
        get => (double?)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    public double? Height
    {
        get => (double?)GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    public Thickness? Margin
    {
        get => (Thickness?)GetValue(MarginProperty);
        set => SetValue(MarginProperty, value);
    }

    public ICommand? Selected
    {
        get => (ICommand?)GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }
}
