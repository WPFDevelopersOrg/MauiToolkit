namespace Maui.Toolkitx.Platforms.Windows.Runtimes.User32;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct TPMPARAMS
{
    public uint cbSize;
    public RECT rcExclude;
}
