using Microsoft.Maui.Platform;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowStartupService : IWindowStartupService
{
    public WindowStartupService(Window window, WindowStartup windowStartup)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(windowStartup);
        _Window = window;
        _WindowStartup = windowStartup;

    }

    readonly Window _Window;
    readonly WindowStartup _WindowStartup;


    bool IService.Run()
    {

        return true;
    }

    bool IService.Stop()
    {

        return true;
    }

    bool IWindowStartupService.SetBackdropsKind(BackdropsKind kind)
    {
        throw new NotImplementedException();
    }
}
