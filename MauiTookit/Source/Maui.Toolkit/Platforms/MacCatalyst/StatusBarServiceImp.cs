using CoreGraphics;
using Foundation;
using Maui.Toolkit.Concurrency;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.MacCatalyst.Extensions;
using Maui.Toolkit.Platforms.MacCatalyst.Helpers;
using Maui.Toolkit.Services;
using ObjCRuntime;
using System.Runtime.InteropServices;
using UIKit;

namespace Maui.Toolkit.Platforms;

internal class StatusBarServiceImp : NSObject, IStatusBarService
{
    public StatusBarServiceImp(StatusBarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StatusBarOptions = options;
    }

    readonly StatusBarOptions _StatusBarOptions;
    bool _IsRegisetr = false;

    bool _IsLoaded = false;
    NSObject? _SystemStatusBar;
    NSObject? _StatusBar;
    NSObject? _StatusBarItem;
    NSObject? _StatusBarButton;
    //NSObject? _StatusBarImage;
    NSObject? _NsImage;

    NSObject? _NsApplication;
    UIApplication? _Application;
    UIWindow? _MainWindow;

    IDisposable? _Disposable;
    string? _ImagePath;

    private event EventHandler<EventArgs>? StatusBarEventChanged;

    event EventHandler<EventArgs> IStatusBarService.StatusBarEventChanged
    {
        add => StatusBarEventChanged += value;
        remove => StatusBarEventChanged -= value;
    }

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddMac(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {
                //if (_IsRegisetr)
                //    return;

                //_IsRegisetr = true;

                //_Application = app;
                //_MainWindow = _Application.Delegate.GetWindow();

                //LoadStatusBar();
                //((IStatusBarService)this).Show(_StatusBarOptions.IconFilePath);

            }).OnResignActivation(app =>
            {

            }).ContinueUserActivity((app, user, handler) =>
            {

                return true;

            }).DidEnterBackground(app =>
            {

            }).WillFinishLaunching((app, options) =>
            {
                _Application = app;
                _NsApplication = UIWindowExtension.GetSharedNsApplication();

                UIWindow.Notifications.ObserveDidBecomeVisible(WindowDidBecomeVisible);

                return true;
            }).FinishedLaunching((app, options) =>
            {
                return true;
            }).OpenUrl((app, url, options) =>
            {
                return true;
            }).PerformActionForShortcutItem((app, item, handler) =>
            {

            }).WillEnterForeground(app =>
            {

            }).WillTerminate(app =>
            {

            }).SceneWillConnect((scrne, session, options) =>
            {

            }).SceneDidDisconnect(scene =>
            {

            });
        });

        return true;
    }

    bool LoadStatusBar()
    {
        if (_IsLoaded)
            return true;

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

        _StatusBarButton.SetValueForNsobject<IntPtr>("setTarget:", this.Handle);
        _StatusBarButton.SetValueForNsobject<IntPtr>("setAction:", new Selector("handleButtonClick:").Handle);

        _IsLoaded = true;

        return true;
    }

    NSObject? LoadImage(string? image)
    {
        if (string.IsNullOrEmpty(image))
            return default;

        if (_ImagePath == image)
            return default;

        var statusBarImage = RuntimeHelper.Alloc("NSImage");
        if (statusBarImage is null)
            return default;

        var nsImageObject = statusBarImage.GetNSObjectFromWithArgument<string>("initWithContentsOfFile:", image);
        return nsImageObject;
    }

    bool SetImage(NSObject? nsImage)
    {
        if (_StatusBarButton is null)
            return false;

        IntPtr nsImagePtr = nsImage?.Handle ?? IntPtr.Zero;
        _StatusBarButton.SetValueForNsobject<IntPtr>("setImage:", nsImagePtr);
        _StatusBarButton.SetValueForNsobject<long>("setImagePosition:", 2);

        if (nsImage is not null)
        {
            nsImage.SetValueForNsobject<CGSize>("setSize:", new CGSize(18, 18));
            nsImage.SetValueForNsobject<bool>("setTemplate:", true);
        }

        return true;
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        var nsImagePtr = LoadImage(iconPath);

        if (nsImagePtr is not null)
        {
            if (_NsImage is not null)
                _NsImage.Dealloc();
        }

        _NsImage = nsImagePtr;
        _ImagePath = iconPath;

        return SetImage(_NsImage);
    }

    bool IStatusBarService.Hide() => SetImage(default);

    bool IStatusBarService.SetDescription(string? text)
    {
        if (_StatusBarButton is null)
            return false;

        if (text is null)
            text = string.Empty;

        _StatusBarButton.SetValueForNsobject<string>("setTitle:", text);

        return true;
    }

    IDisposable IStatusBarService.SchedulePeriodic(TimeSpan period, Func<bool, string>? action)
    {
        if (_Disposable is not null)
            return _Disposable;

        var rate = period.TotalMilliseconds;
        if (rate <= 0)
            rate = 500;
        else if (rate > 1000)
            rate = 1000;

        period = TimeSpan.FromMilliseconds(rate);
        var scheduler = new TimestampedScheduler();
        _Disposable = scheduler;

        scheduler.Run(period, (isFlag, canable) =>
        {
            var nsImage = _NsImage;
            if (!canable.IsDisposed)
            {
                var path = action?.Invoke(isFlag);
                var loadNsImage = LoadImage(path);
                if (isFlag)
                    nsImage = loadNsImage;
                else
                {
                    if (loadNsImage is not null)
                        nsImage = loadNsImage;
                }
            }
            else
                _Disposable = null;

            SetImage(nsImage);
        });

        return scheduler;
    }

    /// <summary>
    /// it will trigger when user click the statusBar
    /// </summary>
    /// <param name="senderStatusBarButton"></param>
    [Export("handleButtonClick:")]
    protected void HandleButtonClick(NSObject senderStatusBarButton)
    {
        var sharedApplication = UIWindowExtension.GetSharedNsApplication();
        if (sharedApplication is null)
            return;

        sharedApplication.SetValueForNsobject<bool>("activateIgnoringOtherApps:", true);

        var uiNsWindow = _MainWindow?.GetHostWidnowForUiWindow();
        uiNsWindow?.SetValueForNsobject<IntPtr>("makeKeyAndOrderFront:", this.Handle);

        StatusBarEventChanged?.Invoke(this, new EventArgs());
    }

    void WindowDidBecomeVisible(object? sender, NSNotificationEventArgs args)
    {
        if (_IsRegisetr)
            return;

        if (_MainWindow is null)
            _MainWindow = _Application?.Windows.FirstOrDefault();

        if (_MainWindow is null)
            return;

        _IsRegisetr = true;

        LoadStatusBar();
        ((IStatusBarService)this).Show(_StatusBarOptions.IconFilePath);
    }

}
