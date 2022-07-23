namespace Maui.Toolkitx.Platforms.Windows.Runtimes.User32;

[Flags]
public enum TackMenuFlag : uint
{
    //Use one of the following flags to specify how the function positions the shortcut menu horizontally.
    TPM_CENTERALIGN = 0x0004,
    TPM_LEFTALIGN = 0,
    TPM_RIGHTALIGN = 0x0008,

    //Use one of the following flags to specify how the function positions the shortcut menu vertically.
    TPM_BOTTOMALIGN = 0x0020,
    TPM_TOPALIGN = 0x0000,
    TPM_VCENTERALIGN = 0x0010,

    //Use the following flags to control discovery of the user selection without having to set up a parent window for the menu.
    TPM_NONOTIFY = 0x0080,
    TPM_RETURNCMD = 0x0100,

    //Use one of the following flags to specify which mouse button the shortcut menu tracks.
    TPM_LEFTBUTTON = 0x0000,
    TPM_RIGHTBUTTON = 0x0002,

    //Use any reasonable combination of the following flags to modify the animation of a menu. For example, by selecting a horizontal and a vertical flag, you can achieve diagonal animation.
    TPM_HORNEGANIMATION = 0x0800,
    TPM_HORPOSANIMATION = 0x0400,
    TPM_NOANIMATION = 0x4000,
    TPM_VERNEGANIMATION = 0x2000,
    TPM_VERPOSANIMATION = 0x1000,

    //Use one of the following flags to specify whether to accommodate horizontal or vertical alignment.
    TPM_HORIZONTAL = 0x0000,
    TPM_VERTICAL = 0x0040,
}
