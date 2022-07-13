namespace Maui.Toolkitx.Extensions;
public static class XMauiContextExtensions
{
    public static IMauiContext RequireMauiContext(this Element element, bool fallbackToAppMauiContext = false)
    => element.FindMauiContext(fallbackToAppMauiContext) ?? throw new InvalidOperationException($"{nameof(IMauiContext)} not found.");

    public static IMauiContext? FindMauiContext(this Element element, bool fallbackToAppMauiContext = false)
    {
        if (element is IElement fe && fe.Handler?.MauiContext != null)
            return fe.Handler.MauiContext;

        foreach (var parent in element.GetParentsPath())
        {
            if (parent is IElement parentView && parentView.Handler?.MauiContext != null)
                return parentView.Handler.MauiContext;
        }

        return fallbackToAppMauiContext ? Application.Current?.FindMauiContext() : default;
    }


}