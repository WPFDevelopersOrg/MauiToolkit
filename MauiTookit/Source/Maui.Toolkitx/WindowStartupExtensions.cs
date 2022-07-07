using Maui.Toolkitx.Providers;

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

    public static IWindowStartupService? GetWindowStartupService(this Window window)
    {
        if (window == null)
            return default;

        var worker = WindowStartupWorker.GetWindowStartupWorker(window);
        if (worker is not IProvider<IWindowStartupService> provider)
            return default;

        return provider.GetService();
    }
}
