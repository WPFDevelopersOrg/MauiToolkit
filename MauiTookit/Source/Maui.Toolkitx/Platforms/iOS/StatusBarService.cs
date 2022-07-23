using Maui.Toolkitx.Config;
using Maui.Toolkitx.Disposables;

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
        return true;
    }

    bool IService.Stop()
    {
        return true;
    }

    IDisposable IStatusBarService.Blink(TimeSpan period, Func<bool, string>? action)
    {
        return new NullDisposable();
    }

    bool IStatusBarService.StopBlink()
    {
        return true;
    }
}
