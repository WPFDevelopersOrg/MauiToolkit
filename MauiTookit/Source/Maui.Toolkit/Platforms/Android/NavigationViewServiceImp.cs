using Maui.Toolkit.Core;
using Maui.Toolkit.Options;
using Maui.Toolkit.Providers;
using Maui.Toolkit.Services;

namespace Maui.Toolkit.Platforms;

internal class NavigationViewServiceImp : INavigationViewService, INatvigationViewProvider
{
    public NavigationViewServiceImp(ShellViewOptions options)
    {

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

    INavigationViewBuilder? INatvigationViewProvider.CreateNavigationViewBuilder(in VisualElement visualElement)
    {
        return default;
    }

    INavigationViewBuilder? INatvigationViewProvider.CreateShellViewBuilder(in Window window)
    {
        return default;
    }

    INavigationViewBuilder? INatvigationViewProvider.GetRootShellViewBuilder()
    {
        return default;
    }

    bool INavigationViewService.SetAppIcon(string icon)
    {
        return true;
    }

    bool INavigationViewService.SetBackButtonVisible(bool isVisible)
    {
        return true;
    }

    bool INavigationViewService.SetBackground(Brush brush)
    {
        return true;
    }

    bool INavigationViewService.SetBackgroundColor(Color color)
    {
        return true;
    }

    bool INavigationViewService.SetContentBackground(Brush brush)
    {
        return true;
    }

    bool INavigationViewService.SetContentBackgroundColor(Color color)
    {
        return true;
    }

    bool INavigationViewService.SetSearchBarVisible(bool isVisible)
    {
        return true;
    }

    bool INavigationViewService.SetSettingsVisible(bool isVisible)
    {
        return true;
    }

    bool INavigationViewService.SetTitle(string title)
    {
        return true;
    }

    bool INavigationViewService.SetTitleBarFontSize(double size)
    {
        return true;
    }

    bool INavigationViewService.SetTitleBarHeight(double height)
    {
        return true;
    }

    bool INavigationViewService.SetToggleButtonVisible(bool isVisible)
    {
        return true;
    }
}
