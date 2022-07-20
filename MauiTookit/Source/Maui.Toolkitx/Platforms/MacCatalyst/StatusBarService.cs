using AppKit;
using Foundation;
using Maui.Toolkitx.Config;
using UIKit;

namespace Maui.Toolkitx;
internal partial class StatusBarService : IService
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
    NSImage? _NsImage;
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
                _Application = app;
                ((IService)this).Run();
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
        LoadStatusBar();
        SetImage(_Config.Icon1);
        return true;
     }

    bool IService.Stop()
    {
        SetImage(default);
        UnloadStatusBar();
        return true;
    }


}
