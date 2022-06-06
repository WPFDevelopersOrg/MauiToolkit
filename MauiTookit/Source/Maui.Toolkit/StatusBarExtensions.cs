using Maui.Toolkit.Options;
using System.Diagnostics;

namespace Maui.Toolkit;
public static class StatusBarExtensions
{
    public static MauiAppBuilder UseStatusBar(this MauiAppBuilder builder) => UseStatusBar(builder, default);

    public static MauiAppBuilder UseStatusBar(this MauiAppBuilder builder, Action<StatusBarOptions>? configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        var processName = Process.GetCurrentProcess().ProcessName;
        var options = new StatusBarOptions()
        {
            Title = processName,
            ToolTipText = processName,
        };
        configureDelegate?.Invoke(options);

        return builder;
    }

}
