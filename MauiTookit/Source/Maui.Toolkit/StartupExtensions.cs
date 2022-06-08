using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;

#if WINDOWS || MACCATALYST || IOS || ANDROID
using Maui.Toolkit.Platforms;
#endif

namespace Maui.Toolkit;

// All the code in this file is included in all platforms.
public static class StartupExtensions
{
    public static MauiAppBuilder UseWindowStartup(this MauiAppBuilder builder) => UseWindowStartup(builder, default);

    public static MauiAppBuilder UseWindowStartup(this MauiAppBuilder builder, Action<StartupOptions>? configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        var options = new StartupOptions()
        {
            TitleBarKind = WindowTitleBarKind.ExtendsContentIntoTitleBar,
            PresenterKind = WindowPresenterKind.Maximize,
            Location = WidnowAlignment.Center,
            Size = new Size(1000, 500),
        };
        configureDelegate?.Invoke(options);

#if WINDOWS || MACCATALYST || IOS || ANDROID
        var vService = new WindowsServiceImp(options);
        builder.Services.AddSingleton<IWindowsService>(vService);
        builder.ConfigureLifecycleEvents(lefecycleEvent =>
        {
            vService.RegisterApplicationEvent(lefecycleEvent);
        });
#endif
        return builder;

    }
}
