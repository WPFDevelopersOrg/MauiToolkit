using Maui.Toolkit.Options;
using Maui.Toolkit.Services;

#if WINDOWS || MACCATALYST || IOS || ANDROID
using Maui.Toolkit.Platforms;
#endif

namespace Maui.Toolkit;
public static class StatusBarExtensions
{
    public static MauiAppBuilder UseStatusBar(this MauiAppBuilder builder) => UseStatusBar(builder, default);

    public static MauiAppBuilder UseStatusBar(this MauiAppBuilder builder, Action<StatusBarOptions>? configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        var appName = PlatformShared.GetApplicationName();
        var options = new StatusBarOptions()
        {
            Title = appName,
            ToolTipText = appName,
        };
        configureDelegate?.Invoke(options);

#if WINDOWS || MACCATALYST || IOS || ANDROID
        var vService = new StatusBarServiceImp(options);
        builder.Services.AddSingleton<IStatusBarService>(vService);
        builder.ConfigureLifecycleEvents(lefecycleEvent =>
        {
            vService.RegisterApplicationEvent(lefecycleEvent);
        });
#endif

        return builder;
    }

}
