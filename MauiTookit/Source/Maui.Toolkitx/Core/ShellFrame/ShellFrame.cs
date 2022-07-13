namespace Maui.Toolkitx;
public partial class ShellFrame
{
    public static readonly BindableProperty ShellFrameProperty =
                           BindableProperty.CreateAttached("ShellFrame", typeof(ShellFrame), typeof(ShellFrame), default, propertyChanged: ShellFramePropertyChanged);


    public static ShellFrame GetShellFrame(Window target) => (ShellFrame)target.GetValue(ShellFrameProperty);
    public static void SetShellFrame(Window target, ShellFrame value) => target.SetValue(ShellFrameProperty, value);

    public static void Remove(Window target)
    {
        target.SetValue(ShellFrameProperty, null);
        target.RemoveBinding(ShellFrame.ShellFrameProperty);
    }

    private static void ShellFramePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Window window)
            return;

        if (ReferenceEquals(oldValue, newValue))
            return;

        if (newValue is ShellFrame shellFrame)
        {
            var shellFrameWorker = ShellFrameWorker.GetShellFrameWorker(window);
            shellFrameWorker?.Detach();

            shellFrameWorker = new ShellFrameWorker(shellFrame);
            ShellFrameWorker.SetShellFrameWorker(window, shellFrameWorker);

            shellFrameWorker.Attach(bindable);
        }
        else
        {
            var shellFrameWorker = ShellFrameWorker.GetShellFrameWorker(window);
            if (shellFrameWorker is null)
                return;

            shellFrameWorker.Detach();
            bindable.RemoveBinding(ShellFrameWorker.ShellFrameWorkerProperty);
        }
    }
}
