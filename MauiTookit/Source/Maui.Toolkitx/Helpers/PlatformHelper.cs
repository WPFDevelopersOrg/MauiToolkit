namespace Maui.Toolkitx.Helpers;

public class PlatformHelper
{
    internal static IService? GetPlatformWindowChromeSevice(Window window, WindowChrome windowChrome)
    {
        if (window is null)
            return default;

        if (window.Handler is null)
            return default;

#if WINDOWS || MACCATALYST || IOS || ANDROID
        return new WindowChromeService(window, windowChrome);
#else
        return default;
#endif
    }

    internal static IService? GetPlatformWindowStartupSevice(Window window, WindowStartup windowStartup)
    {
        if (window is null)
            return default;

        if (window.Handler is null)
            return default;

#if WINDOWS || MACCATALYST || IOS || ANDROID
        return new WindowStartupService(window, windowStartup);
#else
        return default;
#endif
    }

    internal static IService? GetPlatformWindowStartupSevice(Window window, IElementHandler handler, WindowStartup windowStartup)
    {
        if (window is null)
            return default;

        if (handler is null)
            return default;

#if WINDOWS || MACCATALYST || IOS || ANDROID
        return new WindowStartupService(window, handler, windowStartup);
#else
        return default;
#endif
    }

    internal static IService? GetShellViewService(Window window, ShellView shellView)
    {
        if (window is null)
            return default;

        if (window.Handler is null)
            return default;

#if WINDOWS || MACCATALYST || IOS || ANDROID
        return new ShellViewService(window, shellView);
#else
        return default;
#endif
    }

}
