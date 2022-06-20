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
