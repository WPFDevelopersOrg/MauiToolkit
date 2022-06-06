using CoreGraphics;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using Microsoft.Maui.LifecycleEvents;
using ObjCRuntime;
using System.Diagnostics.CodeAnalysis;
using UIKit;

namespace Maui.Toolkit.Platforms;

internal class WindowsServiceImp : IWindowsService
{
    public WindowsServiceImp(StartupOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StartupOptions = options;
    }

    readonly StartupOptions _StartupOptions;
    readonly double _ScalingFactorX = 1.3d;
    readonly double _ScalingFactorY = 1.16d;
    UIApplication? _Application;
    UIWindow? _MainWindow;

    bool _IsRegisetr = false;

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddMac(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {
                if (_IsRegisetr)
                    return;

                _Application = app;
                _MainWindow = app.Windows.FirstOrDefault();

                RemoveTitltBar(_StartupOptions.TitleBarKind);
                MoveWindow(_StartupOptions.PresenterKind);

                _IsRegisetr = true;

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

            }).SceneWillConnect((screen, session, options) =>
            {

            }).SceneDidDisconnect(scene =>
            {

            });
        });

        return true;
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    bool RemoveTitltBar(WindowTitleBarKind titleBar)
    {
        if (_MainWindow is null)
            return false;

        switch (titleBar)
        {
            case WindowTitleBarKind.Default:
                break;
            case WindowTitleBarKind.PlatformDefault:
                break;
            case WindowTitleBarKind.ExtendsContentIntoTitleBar:
                var vTitleBar = _MainWindow.WindowScene?.Titlebar;
                if (vTitleBar is null)
                    return false;

                vTitleBar.TitleVisibility = UITitlebarTitleVisibility.Hidden;
                vTitleBar.Toolbar = null;
                break;
            default:
                break;
        }

        return true;
    }


    bool MoveWindow(WindowPresenterKind presenter) => presenter switch
    {
        WindowPresenterKind.Default => MoveWindow(_StartupOptions.Location, _StartupOptions.Size),
        WindowPresenterKind.Maximize => MoveWindowMaximize(),
        WindowPresenterKind.Minimize => MoveWindowMinimize(),
        WindowPresenterKind.FullScreen => ToggleFullScreen(true),
        _ => false,
    };

    bool MoveWindow(WidnowAlignment location, Size size)
    {
        if (_MainWindow is null)
            return false;

        var vScreen = _MainWindow.Screen;
        var vCGRect = vScreen.Bounds;

        var vSizeRestrictions = _MainWindow.WindowScene?.SizeRestrictions;
        if (vSizeRestrictions is null)
            return false;

        var width = size.Width;
        var height = size.Height;

        if (width < 0)
            width = 0;

        if (height < 0)
            height = 0;

        if (width > vCGRect.Width)
            width = vCGRect.Width;

        if (height > vCGRect.Height)
            height = vCGRect.Height;

        var vRealWidth = width * _ScalingFactorX;
        var vRealHeight = height * _ScalingFactorY;

        switch (location)
        {
            case WidnowAlignment.LeftTop:
                break;
            case WidnowAlignment.RightTop:
                break;
            case WidnowAlignment.Center:
                break;
            case WidnowAlignment.LeftBottom:
                break;
            case WidnowAlignment.RightBottom:
                break;
            default:
                break;
        }

        vSizeRestrictions.MinimumSize = new CGSize(vRealWidth, vRealHeight);
        vSizeRestrictions.MaximumSize = new CGSize(vRealWidth, vRealHeight);

        return true;
    }

    bool MoveWindowMaximize()
    {
        if (_MainWindow is null)
            return false;

        var vScreen = _MainWindow.Screen;
        var vCGRect = vScreen.Bounds;

        double width = vCGRect.Width.Value * _ScalingFactorX;
        double height = vCGRect.Height.Value * _ScalingFactorY;

        //var vCoordinateSpace = _MainWindow.WindowScene.CoordinateSpace;
        //var vBounds = vCoordinateSpace.Bounds;
        //var vStatusBar = _Application.StatusBarFrame;

        //_Application.StatusBarHidden = false;
        var vSizeRestrictions = _MainWindow.WindowScene?.SizeRestrictions;
        if (vSizeRestrictions is null)
            return false;

        //_Application.SetStatusBarHidden(true, true);

        //_MainWindow.CanResizeToFitContent = true;
        //_MainWindow.RootViewController.PreferredContentSize = new CGSize(vBounds.Width * _ScalingFactor, vBounds.Height * _ScalingFactor);
        //_MainWindow.Bounds = new CGRect(0, 0, vBounds.Width * _ScalingFactor, vBounds.Height * _ScalingFactor);

        //_MainWindow.Frame = new CGRect(0, 0, vBounds.Width * _ScalingFactor, vBounds.Height * _ScalingFactor);
        //_MainWindow.AccessibilityFrame = new CGRect(0, 0, vBounds.Width * _ScalingFactor, vBounds.Height * _ScalingFactor);
        //var vNewRect = vCoordinateSpace.ConvertRectFromCoordinateSpace(vCGRect, vCoordinateSpace);
        //_MainWindow.SetNeedsDisplayInRect(new CGRect(0, 0, vBounds.Width * _ScalingFactor, vBounds.Height * _ScalingFactor));
        //_MainWindow.SizeThatFits(new CGSize(vBounds.Width * _ScalingFactor, vBounds.Height * _ScalingFactor));
        //_MainWindow.App

        var vSize = new CGSize(width, height);
        vSizeRestrictions.MinimumSize = vSize;
        vSizeRestrictions.MaximumSize = vSize;
        return true;
    }

    bool MoveWindowMinimize()
    {
        if (_MainWindow is null)
            return false;

        var vSizeRestrictions = _MainWindow.WindowScene?.SizeRestrictions;
        if (vSizeRestrictions is null)
            return false;

        var vSize = new CGSize(0, 0);
        vSizeRestrictions.MinimumSize = vSize;
        vSizeRestrictions.MaximumSize = vSize;

        return true;
    }

    bool MoveWindowRestore()
    {
        if (_MainWindow is null)
            return false;

        var vSizeRestrictions = _MainWindow.WindowScene?.SizeRestrictions;
        if (vSizeRestrictions is null)
            return false;

        vSizeRestrictions.MinimumSize = _StartupOptions.Size;
        vSizeRestrictions.MaximumSize = new Size(double.MaxValue, double.MaxValue);

        return true;
    }

    bool ToggleFullScreen(bool bFullScreen)
    {
        if (_MainWindow is null)
            return false;

        var nsApplication = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
        if (nsApplication is null)
            return false;

        var sharedApplication = nsApplication.PerformSelector(new Selector("sharedApplication"));
        if (sharedApplication is null)
            return false;

        var delegeteSelector = new Selector("delegate");
        if (!sharedApplication.RespondsToSelector(delegeteSelector))
            return false;

        var delegeteIntptr = RuntimeInterop.IntPtr_objc_msgSend(sharedApplication.Handle, delegeteSelector.Handle);
        var delegateObject = Runtime.GetNSObject(delegeteIntptr);

        if (delegateObject is null)
            return false;

        var hostWindowForUIWindowSelector = new Selector("_hostWindowForUIWindow:");
        if (!delegateObject.RespondsToSelector(hostWindowForUIWindowSelector))
            return false;

        var mainWindow = delegateObject.PerformSelector(hostWindowForUIWindowSelector, _MainWindow.Self);
        if (mainWindow is null)
            return false;

        var toggleFullScreenSelector = new Selector("toggleFullScreen:");
        if (!mainWindow.RespondsToSelector(toggleFullScreenSelector))
            return false;

        RuntimeInterop.void_objc_msgSend_IntPtr(mainWindow.Handle, toggleFullScreenSelector.Handle, IntPtr.Zero);

        return true;
    }


    bool IWindowsService.ResizeWindow(Size size) => MoveWindow(_StartupOptions.Location, size);

    bool IWindowsService.RestoreWindow() => MoveWindowRestore();

    bool IWindowsService.SetWindowMaximize() => MoveWindowMaximize();

    bool IWindowsService.SetWindowMinimize() => MoveWindowMinimize();

    bool IWindowsService.SwitchWindow(bool fullScreen) => ToggleFullScreen(fullScreen);
}
