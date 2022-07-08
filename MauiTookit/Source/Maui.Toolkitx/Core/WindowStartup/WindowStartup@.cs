using Maui.Toolkitx.Config;
using Maui.Toolkitx.Controls;
using System.Runtime.CompilerServices;

namespace Maui.Toolkitx;
public partial class WindowStartup : BindableObject
{
    public static readonly BindableProperty WidthProperty =
                          BindableProperty.Create(propertyName: nameof(Width),
                                                  returnType: typeof(double),
                                                  declaringType: typeof(WindowStartup),
                                                  defaultValue: 800d,
                                                  propertyChanged: OnProperyChanged);

    public static readonly BindableProperty HeightProperty =
                           BindableProperty.Create(propertyName: nameof(Height),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: 500d,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowPresenterKindProperty =
                           BindableProperty.Create(propertyName: nameof(WindowPresenterKind),
                                                   returnType: typeof(WindowPresenterKind),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty TopMostProperty =
                           BindableProperty.Create(propertyName: nameof(TopMost),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty ShowInSwitcherProperty =
                           BindableProperty.Create(propertyName: nameof(ShowInSwitcher),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: true,
                                                   propertyChanged: OnProperyChanged);


    public static readonly BindableProperty WindowAlignmentProperty =
                           BindableProperty.Create(propertyName: nameof(WindowAlignment),
                                                   returnType: typeof(WindowAlignment),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty BackdropsKindProperty =
                           BindableProperty.Create(propertyName: nameof(BackdropsKind),
                                                   returnType: typeof(BackdropsKind),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty IsShowFllowMouseProperty =
                           BindableProperty.Create(propertyName: nameof(IsShowFllowMouse),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: true,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty BackdropConfigurationsProperty =
                           BindableProperty.Create(propertyName: nameof(BackdropConfigurations),
                                                   returnType: typeof(BackdropConfigurations),
                                                   declaringType: typeof(WindowStartup),
                                                   defaultValue: new BackdropConfigurations() { IsHighContrast = false,  IsUseBaseKind = true, LuminosityOpacity = 1f, TintOpacity = 0.5f },
                                                   propertyChanged: OnProperyChanged);

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

    public WindowPresenterKind WindowPresenterKind
    {
        get => (WindowPresenterKind)GetValue(WindowPresenterKindProperty);
        set => SetValue(WindowPresenterKindProperty, value);
    }

    public bool TopMost
    {
        get => (bool)GetValue(TopMostProperty);
        set => SetValue(TopMostProperty, value);
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

    public bool IsShowFllowMouse
    {
        get => (bool)GetValue(IsShowFllowMouseProperty);
        set => SetValue(IsShowFllowMouseProperty, value);
    }

    public BackdropsKind BackdropsKind
    {
        get => (BackdropsKind)GetValue(BackdropsKindProperty);
        set => SetValue(BackdropsKindProperty, value);
    }

    public BackdropConfigurations BackdropConfigurations
    {
        get => (BackdropConfigurations)GetValue(BackdropConfigurationsProperty);
        set => SetValue(BackdropConfigurationsProperty, value);
    }


    protected override void OnPropertyChanging([CallerMemberName] string? propertyName = default)
    {
        base.OnPropertyChanging(propertyName);
    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
    }

    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }

}
