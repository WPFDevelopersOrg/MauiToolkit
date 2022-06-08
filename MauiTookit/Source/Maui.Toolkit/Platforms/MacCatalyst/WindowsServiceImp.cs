﻿using CoreGraphics;
using Foundation;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.MacCatalyst.Extensions;
using Maui.Toolkit.Platforms.MacCatalyst.Helpers;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using Microsoft.Maui.LifecycleEvents;
using ObjCRuntime;
using System.Diagnostics.CodeAnalysis;
using UIKit;

namespace Maui.Toolkit.Platforms;

internal class WindowsServiceImp : NSObject, IWindowsService
{
    public WindowsServiceImp(StartupOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StartupOptions = options;
    }

    readonly StartupOptions _StartupOptions;
    readonly double _ScalingFactorX = 1.0d;
    readonly double _ScalingFactorY = 1.0d;
    UIApplication? _Application;
    UIWindow? _MainWindow;
    NSObject? _NsMainWindow;
    NSObject? _NsApplication;
    bool _IsRegisetr = false;
    bool _IsTrigger = false;

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddMac(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {
                if (_IsRegisetr)
                    return;
                _IsRegisetr = true;

                _MainWindow = _Application?.Delegate.GetWindow();

                RemoveTitltBar(_StartupOptions.TitleBarKind);
                //MoveWindow(_StartupOptions.PresenterKind);

            }).OnResignActivation(app =>
            {

            }).ContinueUserActivity((app, user, handler) =>
            {

                return true;

            }).DidEnterBackground(app =>
            {
                // app close
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

                //vTitleBar.ToolbarStyle = UITitlebarToolbarStyle.Automatic;
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
        if (_NsMainWindow is null)
            _NsMainWindow = _MainWindow?.GetHostWidnowForUiWindow();

        if (_NsMainWindow is null)
            return false;

        if (_MainWindow is null)
            return false;

        var vScreen = _MainWindow.Screen;
        var vCGRect = vScreen.Bounds;

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

        var startX = 0d;
        var startY = 0d;
        var realWidth = width * _ScalingFactorX;
        var realHeight = height * _ScalingFactorY;

        switch (location)
        {
            case WidnowAlignment.LeftTop:
                break;
            case WidnowAlignment.RightTop:
                {
                    startX = vCGRect.Width - realWidth;
                    startY = 0;
                }
                break;
            case WidnowAlignment.Center:
                {
                    startX = (vCGRect.Width - realWidth) / 2.0;
                    startY = (vCGRect.Height - realHeight) / 2.0;
                }
                break;
            case WidnowAlignment.LeftBottom:
                {
                    startX = 0;
                    startY = vCGRect.Height - realHeight;
                }
                break;
            case WidnowAlignment.RightBottom:
                {
                    startX = vCGRect.Width - realWidth;
                    startY = vCGRect.Height - realHeight;
                }
                break;
            default:
                break;
        }
        var cgRect = new CGRect(startX, startY, realWidth, realHeight);

        _NsMainWindow.SetValueForNsobject<CGRect, bool>("setFrame:display:", cgRect, true);

        return true;
    }

    bool MoveWindowMaximize()
    {
        if (_NsMainWindow is null)
            return false;

        if (_MainWindow is null)
            return false;

        var vScreen = _MainWindow.Screen;
        var vCGRect = vScreen.Bounds;

        double width = vCGRect.Width.Value * _ScalingFactorX;
        double height = vCGRect.Height.Value * _ScalingFactorY;

        var cgRect = new CGRect(0, 0, width, height);
        _NsMainWindow.SetValueForNsobject<CGRect, bool>("setFrame:display:", cgRect, true);

        return true;
    }

    bool MoveWindowMinimize()
    {
        if (_NsApplication is null)
            return false;

        if (_NsMainWindow is null)
            return false;

        _NsMainWindow.SetValueForNsobject<CGRect, bool>("setFrame:display:", new CGRect(0, 0, _StartupOptions.Size.Width, _StartupOptions.Size.Height), true);
        _NsApplication.SetValueForNsobject<IntPtr>("hide:", _NsMainWindow.Handle);

        return true;
    }

    bool MoveWindowRestore()
    {
 
        return true;
    }

    bool ToggleFullScreen(bool bFullScreen)
    {
        if (_NsMainWindow is null)
            return false;

        _NsMainWindow.SetValueForNsobject<IntPtr>("toggleFullScreen:", this.Handle);

        return true;
    }

    bool IWindowsService.ResizeWindow(Size size) => MoveWindow(_StartupOptions.Location, size);

    bool IWindowsService.RestoreWindow() => MoveWindowRestore();

    bool IWindowsService.SetWindowMaximize() => MoveWindowMaximize();

    bool IWindowsService.SetWindowMinimize() => MoveWindowMinimize();

    bool IWindowsService.SwitchWindow(bool fullScreen) => ToggleFullScreen(fullScreen);

    void  WindowDidBecomeVisible(object? sender, NSNotificationEventArgs args)
    {
        if (_NsMainWindow is null)
            _NsMainWindow = _MainWindow?.GetHostWidnowForUiWindow();

        if (_NsMainWindow is null)
            return;

        if (_IsTrigger)
            return;

        _IsTrigger = true;
        MoveWindow(_StartupOptions.PresenterKind);
    }


}
