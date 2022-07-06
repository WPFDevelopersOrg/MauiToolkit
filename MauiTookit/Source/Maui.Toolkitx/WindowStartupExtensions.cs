namespace Maui.Toolkitx;

internal static class WindowStartupExtensions
{
    public static Window UseWindowStartup(this Window window) => window.UseWindowStartup(default);

    public static Window UseWindowStartup(this Window window, Action<WindowStartup>? configureDelegate)
    {
        var windowStartup = new WindowStartup();
        configureDelegate?.Invoke(windowStartup);
        WindowStartup.SetWindowStartup(window, windowStartup);
        return window;
    }
}
