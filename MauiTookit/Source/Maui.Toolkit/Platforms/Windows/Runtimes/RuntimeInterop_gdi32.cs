using System.Runtime.InteropServices;

namespace Maui.Toolkit.Platforms.Windows.Runtimes;
public static partial class RuntimeInterop
{
    private const string _Gdi32 = "gdi32.dll";

    [DllImport(_Gdi32, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteObject(IntPtr hObject);
}
