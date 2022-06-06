using Maui.Toolkit.Options;

namespace Maui.Toolkit;
public static class MessageNotifyExtensions
{
    public static MauiAppBuilder UseMessageNotify(this MauiAppBuilder builder) => UseMessageNotify(builder, default);

    public static MauiAppBuilder UseMessageNotify(this MauiAppBuilder builder, Action<NotifyOptions>? configureDelegate)
    {
        return builder;
    }

}
