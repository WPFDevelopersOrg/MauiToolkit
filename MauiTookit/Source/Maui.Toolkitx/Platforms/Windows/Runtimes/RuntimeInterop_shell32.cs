using Maui.Toolkitx.Platforms.Windows.Runtimes.Shell32;

namespace Maui.Toolkitx.Platforms.Windows.Runtimes;

public static partial class RuntimeInterop
{
    private const string _Shell32 = "shell32.dll";

    [DllImport(_Shell32, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool Shell_NotifyIcon([In] NotifyCommand dwMessage, [In] ref NOTIFYICONDATA lpData);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dwMessage"></param>
    /// <param name="lpData"></param>
    /// <returns></returns>
    public static bool Shell_NotifyIcon([In] NotifyCommand dwMessage, [In] ref NOTIFYICONDATA? lpData)
    {
        if (lpData is null)
            return false;

        return Shell_NotifyIcon(dwMessage, ref lpData);
    }


    //public unsafe static bool Shell_NotifyIcon(NotifyCommand dwMessage, IntPtr lpData)
    //{
    //    NOTIFYICONDATA* lpData2 = (NOTIFYICONDATA*)lpData.ToPointer();
    //    return Shell_NotifyIcon(dwMessage, lpData2);
    //}

    //public unsafe static bool Shell_NotifyIcon(NotifyCommand dwMessage, ref NOTIFYICONDATA lpData)
    //{
    //    fixed (NOTIFYICONDATA* lpData2 = &lpData)
    //    {
    //        return Shell_NotifyIcon(dwMessage, lpData2);
    //    }
    //}

    [DllImport(_Shell32, CharSet = CharSet.Auto)]
    public static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, string iconPath, ref IntPtr index);


    //[DllImport(_Shell32, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
    //internal static extern int ExtractIconEx(string szExeFileName, int nIconIndex, out IconHandle phiconLarge, out IconHandle phiconSmall, int nIcons);


    //[DllImport(_Shell32, CharSet = CharSet.Auto)]
    //public static extern CopyMe
}
