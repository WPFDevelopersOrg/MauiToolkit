using Maui.Toolkitx.Platforms.Windows.Controllers;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal class WindowChromeService : IWindowChromeService
{
    public WindowChromeService(Window window, WindowChrome windowChrome)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(windowChrome);
        _Window = window;
        _WindowChrome = windowChrome;
    }

    readonly Window _Window;
    readonly WindowChrome _WindowChrome; 

    bool IService.Run()
    {
 
        return true;
    }

    bool IService.Stop()
    {
 
        return true;
    }

}
