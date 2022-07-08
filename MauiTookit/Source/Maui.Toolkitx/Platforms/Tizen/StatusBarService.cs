using Maui.Toolkitx.Config;

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
        throw new NotImplementedException();
    }

    bool IService.Stop()
    {
        throw new NotImplementedException();
    }
}
