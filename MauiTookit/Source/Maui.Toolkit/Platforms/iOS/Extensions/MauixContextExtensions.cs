using UIKit;

namespace Maui.Toolkit.Platforms.iOS.Extensions;

public static partial class MauixContextExtensions
{
    public static UIWindow GetPlatformWindow(this IMauiContext mauiContext) =>
        mauiContext.Services.GetRequiredService<UIWindow>();
}