namespace Maui.Toolkitx;

public partial class WindowChrome
{
    public static readonly BindableProperty WindowChromeProperty =
                           BindableProperty.CreateAttached("WindowChrome", typeof(WindowChrome), typeof(WindowChrome), default, propertyChanged: WindowChromePropertyChanged);

    public static WindowChrome GetWindowChrome(BindableObject target) => (WindowChrome)target.GetValue(WindowChromeProperty);
    public static void SetWindowChrome(BindableObject target, WindowChrome value) => target.SetValue(WindowChromeProperty, value);

    private static void WindowChromePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Window)
            return;

        if (newValue is WindowChrome)
        {
            var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(bindable);
            if (windowChromeWorker is null)
            {
                windowChromeWorker = new WindowChromeWorker();
                WindowChromeWorker.SetWindowChromeWorker(bindable, windowChromeWorker);
            }

            windowChromeWorker.Attach(bindable);
        }
        else
        {
            var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(bindable);
            if (windowChromeWorker is null)
                return;

            windowChromeWorker.Detach();
            bindable.RemoveBinding(WindowChromeWorker.WindowChromeWorkerProperty);
        }
    }
}
