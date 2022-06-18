using Maui.Toolkit.Options;
using Maui.Toolkit.Providers;
using Maui.Toolkit.Services;

#if WINDOWS || MACCATALYST || IOS || ANDROID
using Maui.Toolkit.Platforms;
#endif

namespace Maui.Toolkit;
public static class ShellViewExtensions
{

    public static MauiAppBuilder UseShellViewSettings(this MauiAppBuilder builder) => UseShellViewSettings(builder, default);

    public static MauiAppBuilder UseShellViewSettings(this MauiAppBuilder builder, Action<ShellViewOptions>? configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        var options = new ShellViewOptions
        {

        };
        configureDelegate?.Invoke(options);

#if WINDOWS || MACCATALYST || IOS || ANDROID

        var vService = new NavigationViewServiceImp(options);
        builder.Services.AddSingleton<INavigationViewService>(vService);
        builder.Services.AddSingleton<INatvigationViewProvider>(vService);
        builder.ConfigureLifecycleEvents(lefecycleEvent =>
        {
            vService.RegisterApplicationEvent(lefecycleEvent);
        });

#endif

        return builder;
    }
}
