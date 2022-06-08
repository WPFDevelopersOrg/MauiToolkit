using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Platforms;

internal class NotificationServiceImp : INotificationService
{
    public NotificationServiceImp(NotifyOptions options)
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


    bool INotificationService.AddArgument<T>(string key, T value)
    {
        throw new NotImplementedException();
    }

    bool INotificationService.AddHeader(string id, string title, string arugument)
    {
        throw new NotImplementedException();
    }

    bool INotificationService.AddText(string value)
    {
        throw new NotImplementedException();
    }

    bool INotificationService.AddViewElement<TElement>(NotifyElementKind kind, TElement element)
    {
        throw new NotImplementedException();
    }

    bool INotificationService.Show()
    {
        throw new NotImplementedException();
    }
}
