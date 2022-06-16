using Maui.Toolkit.Platforms.Windows.Runtimes;
using Maui.Toolkit.Platforms.Windows.Runtimes.Shcore;
using Microsoft.Maui.Platform;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT;
using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkit.Platforms.Windows.Extensions;

public static class WinApiExtensions
{
    public static double GetScaleAdjustment(this MicrosoftuiXaml.Window window)
    {
        var hWnd = window?.GetWindowHandle();
        if (hWnd is null || hWnd == IntPtr.Zero)
            return 1;

        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd.Value);
        DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
        IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

        int result = RuntimeInterop.GetDpiForMonitor(hMonitor, MDTFlags.MDT_Default, out uint dpiX, out uint _);
        if (result != 0)
            throw new Exception("Could not get DPI for monitor.");

        uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
        return scaleFactorPercent / 100.0;
    }

    public static IntPtr GetApplicationHandle(this MicrosoftuiXaml.Application app)
    {
        if (app is null)
            return IntPtr.Zero;

        if (app is not IWinRTObject winRTObject)
            return IntPtr.Zero;

        return winRTObject.NativeObject.ThisPtr;
    }


}
