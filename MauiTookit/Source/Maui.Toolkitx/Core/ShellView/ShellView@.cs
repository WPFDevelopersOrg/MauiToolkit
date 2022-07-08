using Maui.Toolkitx.Config;

namespace Maui.Toolkitx;
public partial class ShellView : BindableObject
{
    public static readonly BindableProperty BackButtonVisibleProperty =
                           BindableProperty.Create(propertyName: nameof(BackButtonVisible),
                                                   returnType: typeof(Visibility),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: Visibility.Visible,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty IsPaneToggleButtonVisibleProperty =
                           BindableProperty.Create(propertyName: nameof(IsPaneToggleButtonVisible),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: false,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty IsSettingVisibleProperty =
                           BindableProperty.Create(propertyName: nameof(IsSettingVisible),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: false,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty SettingConfigurationsProperty =
                           BindableProperty.Create(propertyName: nameof(SettingConfigurations),
                                                   returnType: typeof(SettingConfigurations),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty IsSearchBarVisibleProperty =
                           BindableProperty.Create(propertyName: nameof(IsSearchBarVisible),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: false,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty SearchBarConfigurationsProperty =
                           BindableProperty.Create(propertyName: nameof(SearchBarConfigurations),
                                                   returnType: typeof(SearchBarConfigurations),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty BackgroundProperty =
                           BindableProperty.Create(propertyName: nameof(Background),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty ContentBackgroundProperty =
                           BindableProperty.Create(propertyName: nameof(ContentBackground),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(ShellView),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);


    public Visibility BackButtonVisible
    {
        get => (Visibility)GetValue(BackButtonVisibleProperty);
        set => SetValue(BackButtonVisibleProperty, value);
    }

    public bool IsPaneToggleButtonVisible
    {
        get => (bool)GetValue(IsPaneToggleButtonVisibleProperty);
        set => SetValue(IsPaneToggleButtonVisibleProperty, value);
    }

    public bool IsSettingVisible
    {
        get => (bool)GetValue(IsSettingVisibleProperty);
        set => SetValue(IsSettingVisibleProperty, value);
    }

    public SettingConfigurations SettingConfigurations
    {
        get => (SettingConfigurations)GetValue(SettingConfigurationsProperty);
        set => SetValue(SettingConfigurationsProperty, value);
    }

    public bool IsSearchBarVisible
    {
        get => (bool)GetValue(IsSearchBarVisibleProperty);
        set => SetValue(IsSearchBarVisibleProperty, value);
    }
    
    public SearchBarConfigurations SearchBarConfigurations
    {
        get => (SearchBarConfigurations)GetValue(SearchBarConfigurationsProperty);
        set => SetValue(SearchBarConfigurationsProperty, value);
    }

    public Color? Background
    {
        get => (Color?)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public Color? ContentBackground
    {
        get => (Color?)GetValue(ContentBackgroundProperty);
        set => SetValue(ContentBackgroundProperty, value);
    }

    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }
}
