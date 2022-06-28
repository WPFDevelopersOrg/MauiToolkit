namespace Maui.Toolkitx;

public static class ToolkitxExtensions
{
    public static MauiAppBuilder UseMauiToolkitx(this MauiAppBuilder builder) => UseMauiToolkitx(builder, default);

    public static MauiAppBuilder UseMauiToolkitx(this MauiAppBuilder builder, Action<WindowChrome>? configureDelegate)
    {
        return builder;
    }


}
