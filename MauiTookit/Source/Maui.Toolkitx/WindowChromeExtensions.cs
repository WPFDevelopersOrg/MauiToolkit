using Maui.Toolkitx.Providers;

namespace Maui.Toolkitx;

public static class WindowChromeExtensions
{
    public static Window UseWindowChrome(this Window window) => window.UseWindowChrome(default);

    public static Window UseWindowChrome(this Window window, Action<WindowChrome>? configureDelegate)
    {
        var windowChrome = new WindowChrome();
        configureDelegate?.Invoke(windowChrome);
        WindowChrome.SetWindowChrome(window, windowChrome);
        return window;
    }

    public static IWindowChromeService? GetWindowChromeService(this Window window)
    {
        if (window == null)
            return default;

        var worker = WindowChromeWorker.GetWindowChromeWorker(window);
        if (worker is not IProvider<IWindowChromeService> provider)
            return default;

        return provider.GetService();
    }
}
