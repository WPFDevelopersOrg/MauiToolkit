namespace Maui.Toolkitx;

internal partial class WindowChromeWorker
{
    public static readonly BindableProperty WindowChromeWorkerProperty =
                          BindableProperty.CreateAttached("WindowChromeWorker", typeof(WindowChromeWorker), typeof(WindowChromeWorker), default, propertyChanged: WindowChromeWorkerPropertyChanged);

    public static WindowChromeWorker GetWindowChromeWorker(Window target) => (WindowChromeWorker)target.GetValue(WindowChromeWorkerProperty);
    public static void SetWindowChromeWorker(Window target, WindowChromeWorker value) => target.SetValue(WindowChromeWorkerProperty, value);

    private static void WindowChromeWorkerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        
    }

}
