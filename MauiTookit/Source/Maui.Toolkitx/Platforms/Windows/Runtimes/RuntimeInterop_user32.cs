using Maui.Toolkitx.Platforms.Windows.Runtimes.User32;
using System.Runtime.InteropServices;
using static PInvoke.User32;

namespace Maui.Toolkitx.Platforms.Windows.Runtimes;

public static partial class RuntimeInterop
{
    private const string _User32 = "user32.dll";

    [DllImport(_User32, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DestroyIcon(IntPtr handle);

    [DllImport(_User32, CharSet = CharSet.Unicode)]
    public static extern IntPtr CreatePopupMenu();

    [DllImport("user32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool InsertMenuItem(IntPtr hmenu, uint uposition, uint uflags, ref MENUITEMINFO mii);

    [DllImport("user32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool InsertMenu(IntPtr hmenu, int position, MenuItemFlags uflags, IntPtr uIDNewItemOrSubmenu, string text);

    [DllImport("user32")]
    public static extern int SetMenuItemBitmaps(IntPtr hmenu, int nPosition, MenuItemFlags uflags, IntPtr hBitmapUnchecked, IntPtr hBitmapChecked);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool TrackPopupMenuEx(IntPtr hmenu, TackMenuFlag fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DestroyMenu(IntPtr hmenu);

}
