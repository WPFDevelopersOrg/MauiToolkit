using Maui.Toolkit.Platforms.Windows.Runtimes;
using Maui.Toolkit.Platforms.Windows.Runtimes.CoreMessage;
using WindowsSystem = Windows.System;

namespace Maui.Toolkit.Platforms.Windows.Helpers;

public class CoreMessageHelper
{
    private CoreMessageHelper()
    {

    }


    private static CoreMessageHelper? __Instance = default;
    public static CoreMessageHelper Instance => __Instance ??= new CoreMessageHelper();


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
