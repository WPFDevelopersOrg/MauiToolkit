using Maui.Toolkitx.Platforms.Windows.Runtimes;
using Maui.Toolkitx.Platforms.Windows.Runtimes.CoreMessage;
using WindowsSystem = Windows.System;

namespace Maui.Toolkitx.Platforms.Windows.Permanents;
public sealed class SystemDispatcherQueue
{
    private SystemDispatcherQueue()
    {

    }

    private static SystemDispatcherQueue? __Instance = default;
    public static SystemDispatcherQueue Instance => __Instance ??= new();

    private object? _DispatcherQueueController = default;

    public bool EnsureWindowsSystemDispatcherQueueController()
    {
        if (WindowsSystem.DispatcherQueue.GetForCurrentThread() != null)
            return true;

        if (_DispatcherQueueController == null)
        {
            DispatcherQueueOptions options;
            options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
            options.threadType = 2;    // DQTYPE_THREAD_CURRENT
            options.apartmentType = 2; // DQTAT_COM_STA

            object? dispatcherQueueController = default;
            RuntimeInterop.CreateDispatcherQueueController(options, ref dispatcherQueueController);
            _DispatcherQueueController = dispatcherQueueController;
        }

        return true;
    }
}
