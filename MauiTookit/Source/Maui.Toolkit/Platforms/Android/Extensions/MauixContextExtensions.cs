using Android_App = Android.App;

namespace Maui.Toolkit.Platforms.Android.Extensions;

public static class MauixContextExtensions
{
    public static Android_App.Activity GetPlatformWindow(this IMauiContext mauiContext) =>
            mauiContext.Services.GetRequiredService<Android_App.Activity>();
}
