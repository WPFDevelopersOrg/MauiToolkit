using System.Runtime.InteropServices;

namespace Maui.Toolkitx.Platforms.Windows.Runtimes;

public static partial class RuntimeInterop
{
    private const string _User32 = "user32.dll";

    [DllImport(_User32, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DestroyIcon(IntPtr handle);

}
