using CoreGraphics;
using Foundation;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.MacCatalyst.Controllers;
using Maui.Toolkit.Platforms.MacCatalyst.Extensions;
using Maui.Toolkit.Platforms.MacCatalyst.Helpers;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using UIKit;

namespace Maui.Toolkit.Platforms;

internal class WindowsServiceImp : NSObject, IWindowsService
{
    public WindowsServiceImp(StartupOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StartupOptions = options;
    }

    readonly StartupOptions _StartupOptions;

    UIApplication? _Application;
    UIWindow? _MainWindow;
    NSObject? _NsApplication;

    volatile ConcurrentDictionary<UIWindow, Core.IController> _mapWindows = new();

    //[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddMac(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {

            }).OnResignActivation(app =>
            {

            }).ContinueUserActivity((app, user, handler) =>
            {

                return true;

            }).DidEnterBackground(app =>
            {
                // app close
            }).WillFinishLaunching((app, options) =>
            {

                _Application = app;
                _NsApplication = UIWindowExtension.GetSharedNsApplication();

                //var nsAppNotifications = UIWindowExtension.GetNsApplicationNotifications();

                UIWindow.Notifications.ObserveDidBecomeVisible(WindowDidBecomeVisible);
                UIWindow.Notifications.ObserveDidBecomeHidden(WindowDidBecomeHidden);
                UIWindow.Notifications.ObserveDidBecomeKey(WindowDidBecomeKey);


                //var views = NSBundle.MainBundle.LoadNib("Window", default, default);
                //    NSArray* viewArray = [[NSBundle mainBundle] loadNibNamed: @"Window" owner: nil options:nil];
                //app.window = viewArray.firstObject;
                //UIViewController* viewController = [[UIStoryboard storyboardWithName: @"Main" bundle:[NSBundle mainBundle]]instantiateInitialViewController];
                //self.window.rootViewController = viewController;
                //[self.window makeKeyAndVisible] ;

                return true;
            }).FinishedLaunching((app, options) =>
            {
                return true;
            }).OpenUrl((app, url, options) =>
            {
                return true;
            }).PerformActionForShortcutItem((app, item, handler) =>
            {

            }).WillEnterForeground(app =>
            {

            }).WillTerminate(app =>
            {

            }).SceneWillConnect((screen, session, options) =>
            {

            }).SceneDidDisconnect(scene =>
            {

            });
        });

        return true;
    }

    bool IWindowsService.SetBackdrop(BackdropsKind kind)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetBackdrop(kind);
    }

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetTitleBar(kind);
    }

    bool IWindowsService.ResizeWindow(Size size)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.ResizeWindow(size);
    }

    bool IWindowsService.RestoreWindow()
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.RestoreWindow();
    }

    bool IWindowsService.SetWindowMaximize()
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetWindowMaximize();
    }

    bool IWindowsService.SetWindowMinimize()
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetWindowMinimize();
    }

    bool IWindowsService.SwitchWindow(bool fullScreen)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SwitchWindow(fullScreen);
    }

    bool IWindowsService.ShowInTaskBar(bool isShow)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.ShowInTaskBar(isShow);
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    void WindowDidBecomeVisible(object? sender, NSNotificationEventArgs args)
    {
        if (args.Notification.Object is not UIWindow uiWindow)
            return;

        if (_NsApplication is null || _Application is null)
            return;

        if (uiWindow.GetHostWidnowForUiWindow() is null)
            return;

        bool isMainWidnow = false;
        if (_MainWindow is null)
        {
            _MainWindow = uiWindow;
            isMainWidnow = true;
        }

        var controller = _mapWindows.GetOrAdd(uiWindow, window =>
        {
            return new UIKitWindowController(_NsApplication,_Application, window, _StartupOptions, isMainWidnow);
        });
        controller?.Run();
    }

    void WindowDidBecomeHidden(object? sender, NSNotificationEventArgs args)
    {

    }

    void WindowDidBecomeKey(object? sender, NSNotificationEventArgs args)
    {

    }

}
