using Foundation;
using Maui.Toolkit.Platforms.MacCatalyst.Helpers;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using ObjCRuntime;
using UIKit;

namespace Maui.Toolkit.Platforms.MacCatalyst.Extensions;

public static class UIWindowExtension
{
    public static NSObject? GetSharedNsApplication()
    {
        var nsApplication = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
        if (nsApplication is null)
            return default;

        var sharedApplication = nsApplication.PerformSelector(new Selector("sharedApplication"));
        if (sharedApplication is null)
            return default;

        return sharedApplication;
    }

    static UIWindow? testWindow;
    public static NSObject? GetHostWidnowForUiWindow(this UIWindow window)
    {
        if (window is null)
            return default;

        var nsApplication = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
        if (nsApplication is null)
            return default;

        var sharedApplication = nsApplication.PerformSelector(new Selector("sharedApplication"));
        if (sharedApplication is null)
            return default;

        var nsWindows = sharedApplication.GetNsObjectFrom("windows");
        if (nsWindows is null)
            return default;

        if (nsWindows is not NSArray nsArray)
            return default;

        if (nsArray.Count <= 0)
            return default;
        
        var nsArrays = NSArray.FromArray<NSObject>(nsArray);
        if (nsArrays is null)
            return default;

        foreach (var nsObject in nsArrays)
        {
            if (!nsObject.IsKindOfClass(new Class("UINSWindow")))
                continue;

            var nsString = new NSString("uiWindows");
            var forKeyObjects = nsObject.ValueForKey(nsString) as NSArray;
            if (forKeyObjects is null)
                continue;

            var uiWindows = NSArray.FromArray<UIWindow>(forKeyObjects);
            if (uiWindows?.Contains(window) == true)
                return nsObject;
        }

        return default;
    }


    public static NSObject? GetHostWindowForUiWidnowEx(this UIWindow window)
    {
        if (window is null)
            return default;

        var nsApplication = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
        if (nsApplication is null)
            return default;

        var sharedApplication = nsApplication.PerformSelector(new Selector("sharedApplication"));
        if (sharedApplication is null)
            return default;

        var delegeteSelector = new Selector("delegate");
        if (!sharedApplication.RespondsToSelector(delegeteSelector))
            return default;

        var delegeteIntptr = RuntimeInterop.IntPtr_objc_msgSend(sharedApplication.Handle, delegeteSelector.Handle);
        var delegateObject = Runtime.GetNSObject(delegeteIntptr);

        if (delegateObject is null)
            return default;

        var hostWindowForUIWindowSelector = new Selector("_hostWindowForUIWindow:");
        if (!delegateObject.RespondsToSelector(hostWindowForUIWindowSelector))
            return default;

        var mainWindow = delegateObject.PerformSelector(hostWindowForUIWindowSelector, window);

        return mainWindow;
    }

}