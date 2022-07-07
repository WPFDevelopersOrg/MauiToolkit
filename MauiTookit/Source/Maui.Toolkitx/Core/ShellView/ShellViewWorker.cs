namespace Maui.Toolkitx.Core;

internal partial class ShellViewWorker
{
    public static readonly BindableProperty ShellViewWorkerProperty =
                          BindableProperty.CreateAttached("ShellViewWorker", typeof(ShellViewWorker), typeof(ShellViewWorker), default, propertyChanged: ShellViewWorkerPropertyChanged);

    public static ShellViewWorker GetShellViewWorker(Window target) => (ShellViewWorker)target.GetValue(ShellViewWorkerProperty);
    public static void SetShellViewWorker(Window target, ShellViewWorker value) => target.SetValue(ShellViewWorkerProperty, value);

    private static void ShellViewWorkerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }
}
