using System.Runtime.CompilerServices;

namespace Maui.Toolkitx;

public partial class WindowTitleBar : BindableObject
{
    public static readonly BindableProperty TitleBarBackgroundColorProperty =
                       BindableProperty.Create(propertyName: nameof(TitleBarBackgroundColor),
                                               returnType: typeof(Color),
                                               declaringType: typeof(WindowChrome),
                                               defaultValue: default,
                                               propertyChanged: OnProperyChanged);

    public Color TitleBarBackgroundColor
    {
        get => (Color)GetValue(TitleBarBackgroundColorProperty);
        set => SetValue(TitleBarBackgroundColorProperty, value);
    }


    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(bindable);

    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = default)
    {
        base.OnPropertyChanged(propertyName);
    }
}
