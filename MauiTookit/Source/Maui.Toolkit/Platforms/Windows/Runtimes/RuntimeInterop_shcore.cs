using Maui.Toolkit.Platforms.Windows.Runtimes.Shcore;

namespace Maui.Toolkit.Platforms.Windows.Runtimes;

public static partial class RuntimeInterop
{
    private const string _Shcore = "Shcore.dll";

    [DllImport(_Shcore, SetLastError = true)]
    public static extern int GetDpiForMonitor(IntPtr hmonitor, MDTFlags dpiType, out uint dpiX, out uint dpiY);




}
