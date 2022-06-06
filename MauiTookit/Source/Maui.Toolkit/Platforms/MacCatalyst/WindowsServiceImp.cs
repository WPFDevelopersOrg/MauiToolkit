using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;
using UIKit;

namespace Maui.Toolkit.Platforms;

internal class WindowsServiceImp : IWindowsService
{
    public WindowsServiceImp(StartupOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StartupOptions = options;
    }

    readonly StartupOptions _StartupOptions;
    readonly double _ScalingFactorX = 1.3d;
    readonly double _ScalingFactorY = 1.16d;
    UIApplication? _Application;
    UIWindow? _MainWindow;

    bool _IsRegisetr = false;

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddMac(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {
                if (_IsRegisetr)
                    return;

                _Application = app;
                _MainWindow = app.Windows.FirstOrDefault();

                //RemoveTitltBar(_WindowStartupOptions.TitleBar);
                //MoveWindow(_WindowStartupOptions.Presenter);

                _IsRegisetr = true;

            }).OnResignActivation(app =>
            {

            }).ContinueUserActivity((app, user, handler) =>
            {

                return true;

            }).DidEnterBackground(app =>
            {

            }).WillFinishLaunching((app, options) =>
            {
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
