using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Platforms;

internal class NotificationServiceImp : INotificationService
{
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
