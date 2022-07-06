namespace Maui.Toolkitx;

internal partial class WindowStartupWorker
{
    public static readonly BindableProperty WindowStartupWorkerProperty =
                          BindableProperty.CreateAttached("WindowStartupWorker", typeof(WindowStartupWorker), typeof(WindowStartupWorker), default, propertyChanged: WindowStartupWorkerPropertyChanged);

    public static WindowStartupWorker GetWindowStartupWorker(Window target) => (WindowStartupWorker)target.GetValue(WindowStartupWorkerProperty);
    public static void SetWindowStartupWorker(Window target, WindowStartupWorker value) => target.SetValue(WindowStartupWorkerProperty, value);

    private static void WindowStartupWorkerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }

}
