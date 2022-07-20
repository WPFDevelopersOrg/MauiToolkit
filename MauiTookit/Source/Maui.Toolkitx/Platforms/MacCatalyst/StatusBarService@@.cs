using Foundation;
using Maui.Toolkitx.Config;
using UIKit;

namespace Maui.Toolkitx;
internal partial class StatusBarService : IStatusBarService, IService
{

    public StatusBarService(StatusBarConfigurations config)
    {
        ArgumentNullException.ThrowIfNull(config);
        _Config = config;
    }

    readonly StatusBarConfigurations _Config;

    NSObject? _SystemStatusBar;
    NSObject? _StatusBar;
    NSObject? _StatusBarItem;
    NSObject? _StatusBarButton;
    UIApplication? _Application;

    IDisposable? _Disposable;

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

    bool IService.Run()
    {
        throw new NotImplementedException();
    }

    bool IService.Stop()
    {
        throw new NotImplementedException();
    }

    IDisposable IStatusBarService.Blink(TimeSpan period, Func<bool, string>? action)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.StopBlink()
    {
        throw new NotImplementedException();
    }
}
