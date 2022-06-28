namespace Maui.Toolkitx.Helpers;

public class PlatformHelper
{
    public static IWindowService? GetPlatformWindowSevice(Window window, WindowChrome windowChrome)
    {
        if (window is null)
            return default;

        if (window.Handler is null)
            return default;

#if WINDOWS || MACCATALYST || IOS || ANDROID
        return new WindowService(window, windowChrome);
#else
        return default;
#endif
    }

}
