﻿using CoreFoundation;
using CoreGraphics;
using Foundation;
using Maui.Toolkit.Concurrency;
using Maui.Toolkit.Disposables;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.MacCatalyst.Helpers;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;
using ObjCRuntime;
using System.Runtime.InteropServices;

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

        var nsImageObject= statusBarImage.GetNSObjectFromWithArgument<string>("initWithContentsOfFile:", image);
        return nsImageObject;
    }

    bool SetImage(NSObject? nsImage)
    {
        if (_StatusBarButton is null)
            return false;

        IntPtr nsImagePtr = nsImage?.Handle ?? IntPtr.Zero;
        _StatusBarButton.SetValueForNsobject<IntPtr>("setImage:", nsImagePtr);

        if (nsImage is not null)
        {
            nsImage.SetValueForNsobject<CGSize>("setSize:", new CGSize(18, 18));
            nsImage.SetValueForNsobject<bool>("setTemplate:", true);
        }

        _StatusBarButton.SetValueForNsobject<int>("setImagePosition:", 2);
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

        vSharedApplication.SetValueForNsobject<bool>("activateIgnoringOtherApps:", true);
        StatusBarEventChanged?.Invoke(this, new EventArgs());
    }

}
