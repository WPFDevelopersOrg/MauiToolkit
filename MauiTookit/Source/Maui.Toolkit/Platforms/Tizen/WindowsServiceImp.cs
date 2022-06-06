using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using Microsoft.Maui.LifecycleEvents;

namespace Maui.Toolkit.Platforms;

internal class WindowsServiceImp : IWindowsService
{
    public WindowsServiceImp(StartupOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StartupOptions = options;
    }

    readonly StartupOptions _StartupOptions;

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {


        return true;
    }

    bool IWindowsService.ResizeWindow(Size size)
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.RestoreWindow()
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SetWindowMaximize()
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SetWindowMinimize()
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SwitchWindow(bool fullScreen)
    {
        throw new NotImplementedException();
    }
}
