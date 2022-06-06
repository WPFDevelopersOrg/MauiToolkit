using Maui.Toolkit.Options;
using System.Diagnostics;

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

        return builder;
    }

}
