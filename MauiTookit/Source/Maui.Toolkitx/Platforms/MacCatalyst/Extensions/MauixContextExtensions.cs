using UIKit;

namespace Maui.Toolkitx.Platforms.MacCatalyst.Extensions;

public static partial class MauixContextExtensions
{
    public static UIWindow GetPlatformWindow(this IMauiContext mauiContext) =>
        mauiContext.Services.GetRequiredService<UIWindow>();
}