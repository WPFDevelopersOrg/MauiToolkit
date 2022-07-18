using Maui.Toolkitx.Config;
using Maui.Toolkitx.Disposables;

namespace Maui.Toolkitx;

internal partial class StatusBarService : IStatusBarService, IService
{

    public StatusBarService(StatusBarConfigurations config)
    {
        ArgumentNullException.ThrowIfNull(config);
        _Config = config;
    }

    readonly StatusBarConfigurations _Config;

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        
        return true;
    }

    bool IService.Run()
    {
        return true;
    }

    bool IService.Stop()
    {
        return true;
    }

    IDisposable IStatusBarService.Blink(TimeSpan period, Func<bool, string>? action)
    {
        return new NullDisposable();
    }

    bool IStatusBarService.StopBlink()
    {
        return true;
    }
}
