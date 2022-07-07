namespace Maui.Toolkitx.Extensions;
public static class ApplicationExtensions
{
    public static bool IsApplicationOrNull(this object element) => element == null || element is IApplication;

    public static bool IsApplicationOrWindowOrNull(this object element) => element == null || element is IApplication || element is IWindow;
}
