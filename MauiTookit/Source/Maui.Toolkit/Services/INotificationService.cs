using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Services;
public interface INotificationService
{
    /// <summary>
    /// Id is the  group header if id is no same, will not in one group
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="arugument"></param>
    /// <returns></returns>
    bool AddHeader(string id, string title, string arugument);

    bool AddText(string value);

    bool AddArgument<T>(string key, T value);

    bool AddViewElement<TElement>(NotifyElementKind kind, TElement element);

    bool Show();
}
