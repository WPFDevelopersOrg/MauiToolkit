namespace Maui.Toolkitx;
public partial class WindowStartup
{
    public static readonly BindableProperty WindowStartupProperty =
                       BindableProperty.CreateAttached("WindowStartup", typeof(WindowStartup), typeof(WindowStartup), default, propertyChanged: WindowStartupPropertyChanged);

    public static WindowStartup GetWindowStartup(BindableObject target) => (WindowStartup)target.GetValue(WindowStartupProperty);
    public static void SetWindowStartup(BindableObject target, WindowStartup value) => target.SetValue(WindowStartupProperty, value);

    public static void Remove(Window target)
    {
        target.SetValue(WindowStartupProperty, null);
        target.RemoveBinding(WindowStartup.WindowStartupProperty);
    }


    private static void WindowStartupPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Window window)
            return;

        if (ReferenceEquals(oldValue, newValue))
            return;

        if (newValue is WindowStartup windowStartup)
        {
            var windowStartupWorker = WindowStartupWorker.GetWindowStartupWorker(window);
            windowStartupWorker?.Detach();

            windowStartupWorker = new WindowStartupWorker(windowStartup);
            WindowStartupWorker.SetWindowStartupWorker(window, windowStartupWorker);

            windowStartupWorker.Attach(window);
        }
        else
        {
            var windowStartupWorker = WindowStartupWorker.GetWindowStartupWorker(window);
            if (windowStartupWorker is null)
                return;

            windowStartupWorker.Detach();
            bindable.RemoveBinding(WindowStartupWorker.WindowStartupWorkerProperty);
        }
    }
}
