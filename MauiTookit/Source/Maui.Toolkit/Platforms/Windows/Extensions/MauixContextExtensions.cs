using Microsoft.Maui.Platform;

namespace Maui.Toolkit.Platforms.Windows.Extensions;


public static class MauixContextExtensions
{
    public static NavigationRootManager GetNavigationRootManager(this IMauiContext mauiContext) =>
    mauiContext.Services.GetRequiredService<NavigationRootManager>();

    public static Microsoft.UI.Xaml.Window GetPlatformWindow(this IMauiContext mauiContext) =>
        mauiContext.Services.GetRequiredService<Microsoft.UI.Xaml.Window>();

    public static Microsoft.UI.Xaml.Window? GetOptionalPlatformWindow(this IMauiContext mauiContext) =>
        mauiContext.Services.GetService<Microsoft.UI.Xaml.Window>();

    public static IServiceProvider GetApplicationServices(this IMauiContext mauiContext)
    {
        return MauiWinUIApplication.Current.Services
            ?? throw new InvalidOperationException("Unable to find Application Services");
    }


    //public static IMauiContext MakeScoped(this IMauiContext mauiContext, bool registerNewNavigationRoot)
    //{
    //    var scopedContext = new MauiContext(mauiContext.Services);

    //    if (registerNewNavigationRoot)
    //    {
    //        scopedContext.AddWeakSpecific(new NavigationRootManager(scopedContext.GetPlatformWindow()));
    //    }

    //    return scopedContext;
    //}
}