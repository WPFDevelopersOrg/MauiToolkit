namespace Maui.Toolkitx;

public partial class WindowChrome
{
    public static readonly BindableProperty WindowChromeProperty =
                       BindableProperty.CreateAttached("WindowChrome", typeof(WindowChrome), typeof(AppTitleBarExProperty), default, propertyChanged: WindowChromePropertyChanged);

    public static WindowChrome GetWindowChrome(BindableObject target) => (WindowChrome)target.GetValue(WindowChromeProperty);
    public static void SetWindowChrome(BindableObject target, WindowChrome value) => target.SetValue(WindowChromeProperty, value);

    private static void WindowChromePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Window window)
            return;

        if (newValue is WindowChrome windowChrome)
            window.HandlerChanged += Window_HandlerChanged;
        else
            window.HandlerChanged -= Window_HandlerChanged;
    }

    private static void Window_HandlerChanged(object? sender, EventArgs e)
    {
        if (sender is not Window window)
            return;

        var handler = window.Handler;
        if (handler is null)
            return;

        


    }
}
