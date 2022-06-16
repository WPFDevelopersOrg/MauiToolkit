namespace Maui.Toolkit.Helpers;

public static class ServiceProviderHelper
{
    static object __Lock = new();

    public static TService? GetService<TService>()
    {
        if (Current is null)
            return default(TService);

        return Current.GetService<TService>();
    }

    public static IServiceProvider? Current
    {
        get
        {
            lock (__Lock)
            {
#if WINDOWS10_0_17763_0_OR_GREATER
                return MauiWinUIApplication.Current.Services;
#elif ANDROID
                return MauiApplication.Current.Services;
#elif IOS || MACCATALYST
                return  MauiUIApplicationDelegate.Current.Services;
#else
                return null;
#endif
            }
        }
    }


}