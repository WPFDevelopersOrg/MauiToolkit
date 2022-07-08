using Maui.Toolkitx.Config;
using Maui.Toolkitx.Shared;
using Microsoft.Maui.LifecycleEvents;

namespace Maui.Toolkitx;
public static class StatusBarExtensions
{
    public static MauiAppBuilder UseStatusBar(this MauiAppBuilder builder) => UseStatusBar(builder, default);

    public static MauiAppBuilder UseStatusBar(this MauiAppBuilder builder, Action<StatusBarConfigurations>? configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        var appName = PlatformShared.GetApplicationName();
        var options = new StatusBarConfigurations()
        {
            Title = appName,
            ToolTipText = appName,
        };
        configureDelegate?.Invoke(options);

#if WINDOWS || MACCATALYST || IOS || ANDROID
        var vService = new StatusBarService(options);
        builder.Services.AddSingleton<IStatusBarService>(vService);
        builder.ConfigureLifecycleEvents(lefecycleEvent =>
        {
            vService.RegisterApplicationEvent(lefecycleEvent);
        });
#endif

        return builder;
    }
}
