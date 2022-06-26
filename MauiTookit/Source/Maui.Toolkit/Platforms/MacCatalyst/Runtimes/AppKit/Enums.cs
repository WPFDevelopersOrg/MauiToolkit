using ObjCRuntime;

namespace Maui.Toolkit.Platforms.MacCatalyst.Runtimes.AppKit;

[Native]
public enum TitlebarTitleVisibility : long
{
    Visible,
    Hidden,
}


[Flags]
[Native("NSWindowStyleMask")]
public enum NSWindowStyle : ulong
{
    Borderless = 0 << 0,
    Titled = 1 << 0,
    Closable = 1 << 1,
    Miniaturizable = 1 << 2,
    Resizable = 1 << 3,
    Utility = 1 << 4,
    DocModal = 1 << 6,
    NonactivatingPanel = 1 << 7,
    //[Deprecated(PlatformName.MacOSX, 11, 0, message: "Don't use 'TexturedBackground' anymore.")]
    TexturedBackground = 1 << 8,
#if !NET
		[Deprecated (PlatformName.MacOSX, 10, 9, message: "Don't use, this value has no effect.")]
		Unscaled	       					= 1 << 11,
#endif
    UnifiedTitleAndToolbar = 1 << 12,
    Hud = 1 << 13,
    FullScreenWindow = 1 << 14,
    //[Mac(10, 10)]
    FullSizeContentView = 1 << 15
}

[Flags]
[Native]
public enum NSApplicationPresentationOptions : ulong
{
    Default = 0,
    AutoHideDock = (1 << 0),
    HideDock = (1 << 1),

    AutoHideMenuBar = (1 << 2),
    HideMenuBar = (1 << 3),

    DisableAppleMenu = (1 << 4),
    DisableProcessSwitching = (1 << 5),
    DisableForceQuit = (1 << 6),
    DisableSessionTermination = (1 << 7),
    DisableHideApplication = (1 << 8),
    DisableMenuBarTransparency = (1 << 9),

    FullScreen = (1 << 10),
    AutoHideToolbar = (1 << 11),
    //[Mac(10, 11, 2)]
    DisableCursorLocationAssistance = (1 << 12),
}