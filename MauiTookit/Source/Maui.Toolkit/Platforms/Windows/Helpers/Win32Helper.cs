namespace Maui.Toolkit.Platforms.Windows.Helpers;

public class Win32Helper
{
    public static int GET_X_LPARAM(IntPtr lParam) => LOWORD(lParam.ToInt32());

    public static int GET_Y_LPARAM(IntPtr lParam) => HIWORD(lParam.ToInt32());

    public static int HIWORD(int i) => (int)((short)(i >> 16));

    public static int LOWORD(int i) => (int)((short)(i & 65535));

}
