namespace Maui.Toolkitx;

public partial class WindowChrome
{
    public static readonly BindableProperty WindowChromeProperty =
                           BindableProperty.CreateAttached("WindowChrome", typeof(WindowChrome), typeof(WindowChrome), default, propertyChanged: WindowChromePropertyChanged);


    public static WindowChrome GetWindowChrome(Window target) => (WindowChrome)target.GetValue(WindowChromeProperty);
    public static void SetWindowChrome(Window target, WindowChrome value) => target.SetValue(WindowChromeProperty, value);

    public static void Remove(Window target)
    {
        target.SetValue(WindowChromeProperty, null);
        target.RemoveBinding(WindowChrome.WindowChromeProperty);
    }


    private static void WindowChromePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Window window)
            return;

        if (ReferenceEquals(oldValue,newValue))
            return;

        if (newValue is WindowChrome windowChrome)
        {
            var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(window);
            windowChromeWorker?.Detach();

            windowChromeWorker = new WindowChromeWorker(windowChrome);
            WindowChromeWorker.SetWindowChromeWorker(window, windowChromeWorker);

            windowChromeWorker.Attach(bindable);
        }
        else
        {
            var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(window);
            if (windowChromeWorker is null)
                return;

            windowChromeWorker.Detach();
            bindable.RemoveBinding(WindowChromeWorker.WindowChromeWorkerProperty);
        }
    }
}
