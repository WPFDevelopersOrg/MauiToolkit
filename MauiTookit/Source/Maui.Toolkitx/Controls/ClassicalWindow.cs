using Maui.Toolkitx.Config;
using System.Runtime.CompilerServices;

namespace Maui.Toolkitx.Controls;
public class ClassicalWindow : Window
{
    public ClassicalWindow() : base()
    {
        var startup = WindowStartup.GetWindowStartup(this);
        if (startup is null)
        {
            startup = new WindowStartup()
            {
                Width = Width,
                Height = Height,
                WindowPresenterKind = WindowPresenterKind,
                TopMost = TopMost,
                ShowInSwitcher = ShowInSwitcher,
                WindowAlignment = WindowAlignment,
                BackdropsKind = BackdropsKind,
            };

            WindowStartup.SetWindowStartup(this, startup);
        }
    }

    public ClassicalWindow(Page page) : this()
    {
        Page = page;
    }

    public static readonly BindableProperty WidthProperty =
                           BindableProperty.Create(propertyName: nameof(Width),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: 800d,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty HeightProperty =
                           BindableProperty.Create(propertyName: nameof(Height),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: 500d,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowPresenterKindProperty =
                           BindableProperty.Create(propertyName: nameof(WindowPresenterKind),
                                                   returnType: typeof(WindowPresenterKind),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: WindowPresenterKind.Default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty TopMostProperty =
                           BindableProperty.Create(propertyName: nameof(TopMost),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: true,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty ShowInSwitcherProperty =
                           BindableProperty.Create(propertyName: nameof(ShowInSwitcher),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: true,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty WindowAlignmentProperty =
                           BindableProperty.Create(propertyName: nameof(WindowAlignment),
                                                   returnType: typeof(WindowAlignment),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: WindowAlignment.Center,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty IsShowFllowMouseProperty =
                           BindableProperty.Create(propertyName: nameof(IsShowFllowMouse),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: true,
                                                   propertyChanged: OnProperyChanged);


    public static readonly BindableProperty BackdropsKindProperty =
                           BindableProperty.Create(propertyName: nameof(BackdropsKind),
                                                   returnType: typeof(BackdropsKind),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: BackdropsKind.Mica,
                                                   propertyChanged: OnProperyChanged);


    public static readonly BindableProperty BackdropConfigurationsProperty =
                           BindableProperty.Create(propertyName: nameof(BackdropConfigurations),
                                                   returnType: typeof(BackdropConfigurations),
                                                   declaringType: typeof(ClassicalWindow),
                                                   defaultValue: new BackdropConfigurations
                                                                {
                                                                    LuminosityOpacity = 0.5f, 
                                                                    TintColor = Colors.Gray, 
                                                                    TintOpacity = 0.3f
                                                                },
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

        var startup = WindowStartup.GetWindowStartup(this);
        if (startup is null)
            return;

        switch (propertyName)
        {
            case nameof(Width):
                startup.Width = Width;
                break;
            case nameof(Height):
                startup.Height = Height;
                break;
            case nameof(WindowPresenterKind):
                startup.WindowPresenterKind = WindowPresenterKind;
                break;
            case nameof(TopMost):
                startup.TopMost = TopMost;
                break;
            case nameof(ShowInSwitcher):
                startup.ShowInSwitcher = ShowInSwitcher;
                break;
            case nameof(BackdropsKind):
                startup.BackdropsKind = BackdropsKind;
                break;
            case nameof(WindowAlignment):
                startup.WindowAlignment = WindowAlignment;
                break;
            case nameof(IsShowFllowMouse):
                startup.IsShowFllowMouse = IsShowFllowMouse;
                break;
            case nameof(BackdropConfigurations):
                startup.BackdropConfigurations = BackdropConfigurations;
                break;
            default:
                break;
        }
    }

    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {
 
    }

}
