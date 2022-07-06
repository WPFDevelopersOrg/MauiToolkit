namespace Maui.Toolkitx.Helpers;

public class PlatformHelper
{
    public static IWindowChromeService? GetPlatformWindowChromeSevice(Window window, WindowChrome windowChrome)
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

    public static IWindowStartupService? GetPlatformWindowStartupSevice(Window window, WindowStartup windowStartup)
    {
#if WINDOWS || MACCATALYST || IOS || ANDROID
        return new WindowStartupService(window, windowStartup);
#else
        return default;
#endif
    }

}
