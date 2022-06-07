using CoreFoundation;
using CoreGraphics;
using Foundation;
using Maui.Toolkit.Concurrency;
using Maui.Toolkit.Disposables;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;
using ObjCRuntime;

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
                if (_IsRegisetr)
                    return;

                _IsRegisetr = true;
                LoadStatusBar();
                ((IStatusBarService)this).Show(_StatusBarOptions.IconFilePath);

            }).OnResignActivation(app =>
            {

            }).ContinueUserActivity((app, user, handler) =>
            {

                return true;

            }).DidEnterBackground(app =>
            {

            }).WillFinishLaunching((app, options) =>
            {
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

        var statusItemWithLengthSelector = new Selector("statusItemWithLength:");
        if (_SystemStatusBar.RespondsToSelector(statusItemWithLengthSelector))
            _StatusBarItem = Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_nfloat(_SystemStatusBar.Handle, statusItemWithLengthSelector.Handle, 40));

        if (_StatusBarItem is null)
            return false;

        var buttonSelector = new Selector("button");
        if (_StatusBarItem.RespondsToSelector(buttonSelector))
            _StatusBarButton = Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend(_StatusBarItem.Handle, buttonSelector.Handle));

        if (_StatusBarButton is null)
            return false;

        RuntimeInterop.void_objc_msgSend_IntPtr(_StatusBarButton.Handle, Selector.GetHandle("setTarget:"), this.Handle);
        RuntimeInterop.void_objc_msgSend_IntPtr(_StatusBarButton.Handle, Selector.GetHandle("setAction:"), new Selector("handleButtonClick:").Handle);

        _IsLoaded = true;

        return true;
    }

   
    NSObject? LoadImage(string? image)
    {
        if (string.IsNullOrEmpty(image))
            return default;

        if (_ImagePath == image)
            return default;

        var allocSelector = new Selector("alloc");
        var statusBarImage = Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend(Class.GetHandle("NSImage"), allocSelector.Handle));
        if (statusBarImage is null)
            return default;

        var initWithContentsOfFileSelector = new Selector("initWithContentsOfFile:");
        if (!statusBarImage.RespondsToSelector(initWithContentsOfFileSelector))
            return default;

        var imageFilePtr = CFString.CreateNative(image);
        var nsImagePtr = RuntimeInterop.IntPtr_objc_msgSend_IntPtr(statusBarImage.Handle, initWithContentsOfFileSelector.Handle, imageFilePtr);
        CFString.ReleaseNative(imageFilePtr);

        return Runtime.GetNSObject(nsImagePtr);
    }

    bool SetImage(NSObject? nsImage)
    {
        if (_StatusBarButton is null)
            return false;

        IntPtr nsImagePtr = nsImage?.Handle ?? IntPtr.Zero;

        var setImageSelector = new Selector("setImage:");
        if (_StatusBarButton.RespondsToSelector(setImageSelector))
            RuntimeInterop.void_objc_msgSend_IntPtr(_StatusBarButton.Handle, setImageSelector.Handle, nsImagePtr);

        RuntimeInterop.void_objc_msgSend_CGSize(nsImagePtr, Selector.GetHandle("setSize:"), new CGSize(18, 18));
        RuntimeInterop.void_objc_msgSend_bool(nsImagePtr, Selector.GetHandle("setTemplate:"), true);

        var setImagePositionSelector = new Selector("setImagePosition:");
        if (_StatusBarButton.RespondsToSelector(setImagePositionSelector))
            RuntimeInterop.void_objc_msgSend_int(_StatusBarButton.Handle, setImagePositionSelector.Handle, 2);

        return true;
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        var nsImagePtr = LoadImage(iconPath);

        if (nsImagePtr is not null)
        {
            if (_NsImage is not null)
            {
                //_NsImage
            }
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

        var titleSelector = new Selector("setTitle:");
        if (_StatusBarButton.RespondsToSelector(titleSelector))
            RuntimeInterop.void_objc_msgSend_string(_StatusBarButton.Handle, titleSelector.Handle, text);

        return true;
    }

    IDisposable IStatusBarService.SchedulePeriodic(TimeSpan period, Func<bool, string>? action)
    {
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
        var vNsapplication = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
        if (vNsapplication is null)
            return;

        var vSharedApplication = vNsapplication.PerformSelector(new Selector("sharedApplication"));
        if (vSharedApplication is null)
            return;

        RuntimeInterop.void_objc_msgSend_bool(vSharedApplication.Handle, Selector.GetHandle("activateIgnoringOtherApps:"), true);
        StatusBarEventChanged?.Invoke(this, new EventArgs());
    }

}
