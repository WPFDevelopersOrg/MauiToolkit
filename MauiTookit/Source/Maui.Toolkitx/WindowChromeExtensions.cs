namespace Maui.Toolkitx;

public static class WindowChromeExtensions
{
    public static Window UseWindowChrome(this Window window) => UseWindowChrome(window, default);

    public static Window UseWindowChrome(this Window window, Action<WindowChrome>? configureDelegate)
    {
        var windowChrome = new WindowChrome();
        configureDelegate?.Invoke(windowChrome);

        WindowChrome.SetWindowChrome(window, windowChrome);
        return window;
    }
}
