using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using Microsoft.Maui.LifecycleEvents;

namespace Maui.Toolkit.Platforms;

internal class NotificationServiceImp : INotificationService
{
    public NotificationServiceImp(NotifyOptions options)
    {

    }

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
