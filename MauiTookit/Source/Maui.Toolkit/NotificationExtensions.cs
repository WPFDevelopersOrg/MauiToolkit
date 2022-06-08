using Maui.Toolkit.Options;
using Maui.Toolkit.Services;

#if WINDOWS || MACCATALYST || IOS || ANDROID
using Maui.Toolkit.Platforms;
#endif

namespace Maui.Toolkit;
public static class NotificationExtensions
{
    public static MauiAppBuilder UseMessageNotify(this MauiAppBuilder builder) => UseMessageNotify(builder, default);

    public static MauiAppBuilder UseMessageNotify(this MauiAppBuilder builder, Action<NotifyOptions>? configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        var appName = PlatformShared.GetApplicationName();
        var options = new NotifyOptions()
        {
            Title = appName,
        };
        configureDelegate?.Invoke(options);

#if WINDOWS || MACCATALYST || IOS || ANDROID
        var vService = new NotificationServiceImp(options);
        builder.Services.AddSingleton<INotificationService>(vService);
        builder.ConfigureLifecycleEvents(lefecycleEvent =>
        {
            vService.RegisterApplicationEvent(lefecycleEvent);
        });
#endif
        return builder;
    }

}
