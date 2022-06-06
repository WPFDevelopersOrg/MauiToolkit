using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;

namespace Maui.Toolkit.Platforms;

internal class StatusBarServiceImp : IStatusBarService
{
    public StatusBarServiceImp(StatusBarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StatusBarOptions = options;
    }

    readonly StatusBarOptions _StatusBarOptions;

    private event EventHandler<EventArgs>? StatusBarEventChanged;

    event EventHandler<EventArgs> IStatusBarService.StatusBarEventChanged
    {
        add => StatusBarEventChanged += value;
        remove => StatusBarEventChanged -= value;
    }

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


    bool IStatusBarService.Blink(double rate)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.Hide()
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.SetDescription(string? text)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.Stop()
    {
        throw new NotImplementedException();
    }
}
