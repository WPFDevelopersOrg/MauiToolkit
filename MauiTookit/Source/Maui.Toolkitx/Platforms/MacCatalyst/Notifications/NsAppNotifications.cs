using Foundation;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using ObjCRuntime;

namespace Maui.Toolkit.Platforms.MacCatalyst.Notifications;

public static class NsApplication_Notifications
{
    public static NSObject ObserveWillBecomeActive(EventHandler<NSNotificationEventArgs> handler)
    {       
        EventHandler<NSNotificationEventArgs> handler2 = handler;
        var naApplicationWillBecomeActive = Dlfcn.GetStringConstant(Libraries.AppKit.Handle, "NSApplicationWillBecomeActiveNotification");

        return NSNotificationCenter.DefaultCenter.AddObserver(naApplicationWillBecomeActive, delegate (NSNotification notification)
        {
            handler2(null, new NSNotificationEventArgs(notification));
        });
    }

    public static NSObject ObserveWindowCreated(EventHandler<NSNotificationEventArgs> handler)
    {
        EventHandler<NSNotificationEventArgs> handler2 = handler;
        var naApplicationWindowCreated = Dlfcn.GetStringConstant(Libraries.AppKit.Handle, "NSAccessibilityWindowCreatedNotification");
        return NSNotificationCenter.DefaultCenter.AddObserver(naApplicationWindowCreated, delegate (NSNotification notification)
        {
            handler2(null, new NSNotificationEventArgs(notification));
        });
    }


}