namespace Maui.Toolkitx.Platforms.Windows.Runtimes.User32;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct RECT
{
    public int left;
    public int top;
    public int right;
    public int bottom;
}
