using Foundation;
using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using UIKit;
using UserNotifications;

namespace Maui.Toolkit.Platforms;

internal class NotificationServiceImp : INotificationService
{
    public NotificationServiceImp(NotifyOptions options)
    {

    }

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddMac(windowsLeftCycle =>
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
                //when you want to use application badge you must add settings for application this method add after ios8
                var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Badge | UIUserNotificationType.Alert | UIUserNotificationType.Sound, new NSSet());
                app.RegisterUserNotificationSettings(settings);

                //application badge;
                //app.ApplicationIconBadgeNumber = 23;

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

        UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
        {
            if (!approved)
                return;

            var content = new UNMutableNotificationContent()
            {
                Title = "123",
                Body = "232323"
            };

            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);
            var request = UNNotificationRequest.FromIdentifier(Guid.NewGuid().ToString(), content, trigger);
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
            {
                if (err != null)
                    throw new System.Exception($"Failed to schedule notification: {err}");
            });
        });

        throw new NotImplementedException();
    }
}
