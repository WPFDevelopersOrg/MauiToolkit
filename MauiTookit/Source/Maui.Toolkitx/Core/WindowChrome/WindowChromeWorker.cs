namespace Maui.Toolkitx;

internal partial class WindowChromeWorker
{
    public static readonly BindableProperty WindowChromeWorkerProperty =
                          BindableProperty.CreateAttached("WindowChrome", typeof(WindowChromeWorker), typeof(WindowChromeWorker), default, propertyChanged: WindowChromeWorkerPropertyChanged);

    public static WindowChromeWorker GetWindowChromeWorker(BindableObject target) => (WindowChromeWorker)target.GetValue(WindowChromeWorkerProperty);
    public static void SetWindowChromeWorker(BindableObject target, WindowChromeWorker value) => target.SetValue(WindowChromeWorkerProperty, value);

    private static void WindowChromeWorkerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        
    }

}
