using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Platforms;

internal class MessageNotifyServiceImp : IMessageNotifyService
{
    bool IMessageNotifyService.AddArgument<T>(string key, T value)
    {
        throw new NotImplementedException();
    }

    bool IMessageNotifyService.AddHeader(string id, string title, string arugument)
    {
        throw new NotImplementedException();
    }

    bool IMessageNotifyService.AddText(string value)
    {
        throw new NotImplementedException();
    }

    bool IMessageNotifyService.AddViewElement<TElement>(NotifyElementKind kind, TElement element)
    {
        throw new NotImplementedException();
    }

    bool IMessageNotifyService.Show()
    {
        throw new NotImplementedException();
    }
}
