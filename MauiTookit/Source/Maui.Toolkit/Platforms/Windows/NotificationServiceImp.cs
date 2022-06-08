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
        ArgumentNullException.ThrowIfNull(lifecycleBuilder, nameof(lifecycleBuilder));

        lifecycleBuilder.AddWindows(windowsLeftCycle =>
        {
            windowsLeftCycle.OnWindowCreated(window =>
            {
                

            }).OnVisibilityChanged((window, arg) =>
            {

            }).OnActivated((window, arg) =>
            {


            }).OnLaunching((application, arg) =>
            {

            }).OnLaunched((application, arg) =>
            {

            }).OnPlatformMessage((w, arg) =>
            {
                

            }).OnResumed(window =>
            {

            }).OnClosed((window, arg) =>
            {
                ((IStatusBarService)this).Hide();
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
