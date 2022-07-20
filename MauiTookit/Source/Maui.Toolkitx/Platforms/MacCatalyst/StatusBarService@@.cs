using AppKit;
using CoreGraphics;
using Foundation;
using Maui.Toolkitx.Platforms.MacCatalyst.Extensions;
using Maui.Toolkitx.Platforms.MacCatalyst.Helpers;
using ObjCRuntime;

namespace Maui.Toolkitx;

internal partial class StatusBarService : NSObject
{
    bool LoadStatusBar()
    {
        _StatusBar = Runtime.GetNSObject(Class.GetHandle("NSStatusBar"));
        if (_StatusBar is null)
            return false;

        _SystemStatusBar = _StatusBar.PerformSelector(new Selector("systemStatusBar"));
        if (_SystemStatusBar is null)
            return false;

        _StatusBarItem = _SystemStatusBar.GetNSObjectFromWithArgument<NFloat>("statusItemWithLength:", 40);
        if (_StatusBarItem is null)
            return false;

        _StatusBarButton = _StatusBarItem.GetNsObjectFrom("button");
        if (_StatusBarButton is null)
            return false;

        _StatusBarButton.SetValueForNsobject<IntPtr>("setTarget:", Handle);
        _StatusBarButton.SetValueForNsobject<IntPtr>("setAction:", new Selector("handleButtonClick:").Handle);

        return true;
    }

    bool SetImage(string? image)
    {
        if (_StatusBarButton is null)
            return false;

        IntPtr nsImagePtr = IntPtr.Zero;
        if (!string.IsNullOrWhiteSpace(image))
        {
            if (_NsImage is null)
            {
                var statusBarImage = new NSImage(image)
                {
                    Size = new CGSize(18, 18),
                    Template = true,
                };

                _NsImage = statusBarImage;
            }
            else
            {
                if (image != _Config.Icon1)
                {
                    _NsImage?.Dispose();
                    var statusBarImage = new NSImage(image)
                    {
                        Size = new CGSize(18, 18),
                        Template = true,
                    };
                    _NsImage = statusBarImage;
                }
            }

            nsImagePtr = _NsImage.Handle;
        }
        
        _StatusBarButton.SetValueForNsobject<IntPtr>("setImage:", nsImagePtr);
        _StatusBarButton.SetValueForNsobject<long>("setImagePosition:", 2);

        return true;
    }

    bool UnloadStatusBar()
    {
        _NsImage?.Dispose();
        _StatusBarButton?.Dispose();
        _StatusBarItem?.Dispose();
        _SystemStatusBar?.Dispose();
        _StatusBar?.Dispose();

        return true;
    }
    

    [Export("handleButtonClick:")]
    protected void HandleButtonClick(NSObject senderStatusBarButton)
    {
        var mainWindow = _Application?.Windows.FirstOrDefault();
        if (mainWindow is null)
            return;

        var sharedApplication = UIWindowExtension.GetSharedNsApplication();
        if (sharedApplication is null)
            return;

        sharedApplication.SetValueForNsobject<bool>("activateIgnoringOtherApps:", true);

        var uiNsWindow = mainWindow?.GetHostWidnowForUiWindow();
        uiNsWindow?.SetValueForNsobject<IntPtr>("makeKeyAndOrderFront:", this.Handle);

        //StatusBarEventChanged?.Invoke(this, new EventArgs());
    }
}
