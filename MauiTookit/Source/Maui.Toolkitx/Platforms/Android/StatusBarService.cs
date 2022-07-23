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
        lifecycleBuilder.AddAndroid(windowsLeftCycle =>
        {
            windowsLeftCycle.OnApplicationCreating(app =>
            {


            }).OnNewIntent((activity, intent) =>
            {

            }).OnConfigurationChanged((activity, config) =>
            {

            }).OnBackPressed(activity =>
            {
                return true;
            }).OnActivityResult((activity, requestCode, resultCode, data) =>
            {

            }).OnPostResume(activity =>
            {
            }).OnPostCreate((activity, state) =>
            {

            }).OnRestart(activity =>
            {

            }).OnDestroy(activity =>
            {

            }).OnRequestPermissionsResult((activity, requestCode, permissions, grantResults) =>
            {

            }).OnSaveInstanceState((activity, state) =>
            {

            }).OnRestoreInstanceState((activity, state) =>
            {

            }).OnPause(activity =>
            {

            }).OnResume(activity =>
            {

            }).OnStart(activity =>
            {

            }).OnCreate((activity, state) =>
            {

            }).OnStop(activity =>
            {

            }).OnApplicationConfigurationChanged((App, config) =>
            {

            }).OnApplicationTrimMemory((app, level) =>
            {

            }).OnApplicationLowMemory(app =>
            {

            }).OnApplicationCreate(app =>
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
