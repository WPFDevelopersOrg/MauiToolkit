using Foundation;

namespace Maui.Toolkit.Platforms.MacCatalyst.Notifications;

public static class NsApplication_Notifications
{
    public static NSObject ObserveWillBecomeActive(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
    {
        
        EventHandler<NSNotificationEventArgs> handler2 = handler;

        var naApplicationWillBecomeActive = Dlfcn.GetStringConstant(Libraries.AppKit.Handle, "NSApplicationWillBecomeActiveNotification");

        return NSNotificationCenter.DefaultCenter.AddObserver(naApplicationWillBecomeActive, delegate (NSNotification notification)
        {
            handler2(null, new NSNotificationEventArgs(notification));
        });
    }
}