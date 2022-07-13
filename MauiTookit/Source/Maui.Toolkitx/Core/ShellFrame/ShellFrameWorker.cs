namespace Maui.Toolkitx.Core;

internal partial class ShellFrameWorker
{
    public static readonly BindableProperty ShellFrameWorkerProperty =
                          BindableProperty.CreateAttached("ShellFrameWorker", typeof(ShellFrameWorker), typeof(ShellFrameWorker), default, propertyChanged: ShellFrameWorkerPropertyChanged);

    public static ShellFrameWorker GetShellFrameWorker(Window target) => (ShellFrameWorker)target.GetValue(ShellFrameWorkerProperty);
    public static void SetShellFrameWorker(Window target, ShellFrameWorker value) => target.SetValue(ShellFrameWorkerProperty, value);

    private static void ShellFrameWorkerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }
}
