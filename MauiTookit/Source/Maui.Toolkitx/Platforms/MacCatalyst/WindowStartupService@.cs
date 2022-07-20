using Microsoft.Maui.Platform;
using UIKit;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowStartupService : IWindowStartupService, IService
{
    public WindowStartupService(Window window, WindowStartup windowStartup)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(windowStartup);
        if (window.Handler?.PlatformView is not UIWindow uiWindow)
            ArgumentNullException.ThrowIfNull(nameof(UIWindow));
        else
            _PlatformWindow = uiWindow;

        _Window = window;
        _WindowStartup = windowStartup;
    }

    public WindowStartupService(Window window, IElementHandler handler, WindowStartup windowStartup)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(handler);
        ArgumentNullException.ThrowIfNull(windowStartup);

        if (handler.PlatformView is not UIWindow uiWindow)
            ArgumentNullException.ThrowIfNull(nameof(UIWindow));
        else
            _PlatformWindow = uiWindow;

        _Window = window;
        _WindowStartup = windowStartup;
    }

    readonly Window _Window;
    readonly WindowStartup _WindowStartup;
    readonly UIWindow? _PlatformWindow;


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
        return true;
    }
}
