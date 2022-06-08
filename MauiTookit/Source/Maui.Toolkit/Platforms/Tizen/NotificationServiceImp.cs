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


        return true;
    }

    bool IMessageNotifyService.AddArgument<T>(string key, T value)
    {
        return true;
    }

    bool IMessageNotifyService.AddHeader(string id, string title, string arugument)
    {
        return true;
    }

    bool IMessageNotifyService.AddText(string value)
    {
        return true;
    }

    bool IMessageNotifyService.AddViewElement<TElement>(NotifyElementKind kind, TElement element)
    {
        return true;
    }

    bool IMessageNotifyService.Show()
    {
        return true;
    }
}
