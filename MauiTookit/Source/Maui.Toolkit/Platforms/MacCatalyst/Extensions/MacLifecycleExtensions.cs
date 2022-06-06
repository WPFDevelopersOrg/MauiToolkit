namespace Microsoft.Maui.LifecycleEvents;

public static class MacLifecycleExtensions
{
    public static ILifecycleBuilder AddMac(this ILifecycleBuilder builder, Action<IiOSLifecycleBuilder> configureDelegate) => builder.AddiOS(configureDelegate);

}