using Maui.Toolkit.Disposables;
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

    bool IStatusBarService.Hide()
    {
        return true;
    }

    bool IStatusBarService.SetDescription(string? text)
    {
        return true;
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        return true;
    }

    IDisposable IStatusBarService.SchedulePeriodic(TimeSpan period, Func<bool, string>? action)
    {
        return new NullDisposible();
    }
}
