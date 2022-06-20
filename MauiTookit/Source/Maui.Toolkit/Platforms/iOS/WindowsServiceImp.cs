using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;

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
        lifecycleBuilder.AddiOS(windowsLeftCycle =>
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

            }).SceneWillConnect((scrne, session, options) =>
            {

            }).SceneDidDisconnect(scene =>
            {

            });
        });

        return true;
    }

    bool IWindowsService.SetBackdrop(BackdropsKind kind)
    {
        return true;
    }

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind)
    {
        return true;
    }

    bool IWindowsService.ResizeWindow(Size size)
    {
        return true;
    }

    bool IWindowsService.RestoreWindow()
    {
        return true;
    }

    bool IWindowsService.SetWindowMaximize()
    {
        return true;
    }

    bool IWindowsService.SetWindowMinimize()
    {
        return true;
    }

    bool IWindowsService.SwitchWindow(bool fullScreen)
    {
        return true;
    }

    bool IWindowsService.ShowInTaskBar(bool isShow)
    {
        return true;
    }
}
