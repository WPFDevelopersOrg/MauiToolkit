namespace Maui.Toolkitx;

internal partial class WindowTitleBarWorker
{
    public static readonly BindableProperty WindowTitleBarWorkerProperty =
                      BindableProperty.CreateAttached("WindowTitleBarWorker", typeof(WindowTitleBarWorker), typeof(WindowTitleBarWorker), default, propertyChanged: WindowTitleBarWorkerPropertyChanged);

    public static WindowTitleBarWorker GetWindowTitleBarWorker(Window target) => (WindowTitleBarWorker)target.GetValue(WindowTitleBarWorkerProperty);
    public static void SetWindowTitleBarWorker(Window target, WindowTitleBarWorker value) => target.SetValue(WindowTitleBarWorkerProperty, value);

    private static void WindowTitleBarWorkerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }
}
