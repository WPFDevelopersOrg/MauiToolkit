using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;

namespace Maui.Toolkit.Platforms;

internal class StatusBarServiceImp : IStatusBarService
{
    public StatusBarServiceImp(StatusBarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StatusBarOptions = options;
    }

    readonly StatusBarOptions _StatusBarOptions;

    private event EventHandler<EventArgs>? StatusBarEventChanged;

    event EventHandler<EventArgs> IStatusBarService.StatusBarEventChanged
    {
        add => StatusBarEventChanged += value;
        remove => StatusBarEventChanged -= value;
    }

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {


        return true;
    }


    bool IStatusBarService.Blink(double rate)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.Hide()
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.SetDescription(string? text)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.Stop()
    {
        throw new NotImplementedException();
    }
}
