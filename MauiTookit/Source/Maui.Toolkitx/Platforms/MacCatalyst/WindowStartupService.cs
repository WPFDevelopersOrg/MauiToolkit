using Foundation;
using Maui.Toolkitx.Platforms.MacCatalyst.Extensions;
using Microsoft.Maui.Platform;
using UIKit;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowStartupService : IService
{
    public WindowStartupService(Window window, WindowStartup windowStartup)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(windowStartup);
        var platformwindows = window.Handler?.PlatformView as UIWindow;
        ArgumentNullException.ThrowIfNull(platformwindows);
        
        _Window = window;
        _WindowStartup = windowStartup;
        _PlatformWindow = platformwindows;
        _NsApplication = UIWindowExtension.GetSharedNsApplication();

        UIWindow.Notifications.ObserveDidBecomeVisible(WindowDidBecomeVisible);
        //_NsWindow = _PlatformWindow.GetHostWidnowForUiWindow();
    }


    public WindowStartupService(Window window, IElementHandler handler, WindowStartup windowStartup)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(handler);
        ArgumentNullException.ThrowIfNull(windowStartup);
        var platformwindows = handler?.PlatformView as UIWindow;
        ArgumentNullException.ThrowIfNull(platformwindows);

        _Window = window;
        _WindowStartup = windowStartup;
        _PlatformWindow = platformwindows;
        _NsApplication = UIWindowExtension.GetSharedNsApplication();

        UIWindow.Notifications.ObserveDidBecomeVisible(WindowDidBecomeVisible);
        //UIWindow.Notifications.ObserveDidBecomeHidden(WindowDidBecomeHidden);
        //_NsWindow = _PlatformWindow.GetHostWidnowForUiWindow();
    }

    readonly Window _Window;
    readonly WindowStartup _WindowStartup;
    readonly UIWindow _PlatformWindow;

    readonly NSObject? _NsApplication;
    NSObject? _NsWindow;
    bool _IsLoaded;

    readonly double _ScalingFactorX = 1.0d;
    readonly double _ScalingFactorY = 1.0d;

    bool IService.Run()
    {
        LoadApplicationEvent();

        if (_NsWindow is null)
            return true;

        LoadBackgroundMaterial(_WindowStartup.BackdropsKind, _WindowStartup.BackdropConfigurations);
        MoveWindow(_WindowStartup.WindowPresenterKind);
        _IsLoaded = true;
        return true;
    }

    bool IService.Stop()
    {
        _IsLoaded = false;
        _NsWindow = default;
        return true;
    }

   
}
