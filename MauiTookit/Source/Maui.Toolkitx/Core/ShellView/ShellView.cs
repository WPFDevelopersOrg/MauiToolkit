namespace Maui.Toolkitx;
public partial class ShellView
{
    public static readonly BindableProperty ShellViewProperty =
                           BindableProperty.CreateAttached("ShellView", typeof(ShellView), typeof(ShellView), default, propertyChanged: ShellViewPropertyChanged);


    public static ShellView GetShellView(Window target) => (ShellView)target.GetValue(ShellViewProperty);
    public static void SetShellView(Window target, ShellView value) => target.SetValue(ShellViewProperty, value);

    public static void Remove(Window target)
    {
        target.SetValue(ShellViewProperty, null);
        target.RemoveBinding(ShellView.ShellViewProperty);
    }


    private static void ShellViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Window window)
            return;

        if (ReferenceEquals(oldValue, newValue))
            return;

        if (newValue is ShellView ShellView)
        {
            var shellViewWorker = ShellViewWorker.GetShellViewWorker(window);
            shellViewWorker?.Detach();

            shellViewWorker = new ShellViewWorker(ShellView);
            ShellViewWorker.SetShellViewWorker(window, shellViewWorker);

            shellViewWorker.Attach(bindable);
        }
        else
        {
            var shellViewWorker = ShellViewWorker.GetShellViewWorker(window);
            if (shellViewWorker is null)
                return;

            shellViewWorker.Detach();
            bindable.RemoveBinding(ShellViewWorker.ShellViewWorkerProperty);
        }
    }
}
