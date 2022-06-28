using Maui.Toolkitx.Platforms.Windows.Runtimes.CoreMessage;

namespace Maui.Toolkitx.Platforms.Windows.Runtimes;

public static partial class RuntimeInterop
{
    private const string _CoreMessage = "CoreMessaging.dll";

    [DllImport(_CoreMessage)]
    public static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object? dispatcherQueueController);
}
