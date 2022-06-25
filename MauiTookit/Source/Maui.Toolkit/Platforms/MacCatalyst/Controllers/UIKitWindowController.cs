using CoreGraphics;
using Foundation;
using Maui.Toolkit.Core;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.MacCatalyst.Extensions;
using Maui.Toolkit.Platforms.MacCatalyst.Helpers;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes.AppKit;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using UIKit;

namespace Maui.Toolkit.Platforms.MacCatalyst.Controllers;

internal partial class UIKitWindowController : NSObject, IController, IWindowsService
{
    public UIKitWindowController(NSObject nsApplication, UIApplication application, UIWindow uiWindow, StartupOptions options, bool isMainWinodw = false)
    {
        ArgumentNullException.ThrowIfNull(nsApplication, nameof(nsApplication));
        ArgumentNullException.ThrowIfNull(application, nameof(application));
        ArgumentNullException.ThrowIfNull(uiWindow, nameof(uiWindow));
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _NsApplication = nsApplication;
        _Application = application;
        _Window = uiWindow;
        _Options = options;
        _IsMainWindow = isMainWinodw;
        _NsWindow = uiWindow.GetHostWidnowForUiWindow();
    }

    readonly NSObject _NsApplication;
    readonly UIApplication _Application;
    readonly UIWindow _Window;
    readonly StartupOptions _Options;
    readonly bool _IsMainWindow;

    readonly NSObject? _NsWindow;
    readonly double _ScalingFactorX = 1.0d;
    readonly double _ScalingFactorY = 1.0d;

    bool IController.Run()
    {
        LoadBackgroundMaterial(_Options.BackdropsKind);
        MoveWindow(_Options.PresenterKind);
        //RemoveTitleBarEx(_StartupOptions.TitleBarKind);
        RemoveTitleBar(_Options.TitleBarKind);
        return true;
    }

    bool IController.Stop()
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SetBackdrop(BackdropsKind kind)
    {
        return true;
    }

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind)
    {
        return true;
    }

    bool IWindowsService.ResizeWindow(Size size) => MoveWindow(_Options.Location, size);

    bool IWindowsService.RestoreWindow() => MoveWindowRestore();

    bool IWindowsService.SetWindowMaximize() => MoveWindowMaximize();

    bool IWindowsService.SetWindowMinimize() => MoveWindowMinimize();

    bool IWindowsService.SwitchWindow(bool fullScreen) => ToggleFullScreen(fullScreen);

    bool IWindowsService.ShowInTaskBar(bool isShow)
    {
        return true;
    }
}

internal partial class UIKitWindowController
{
    bool LoadBackgroundMaterial(BackdropsKind kind)
    {

        return true;
    }

    bool RemoveTitleBar(WindowTitleBarKind titleBar)
    {
        if (_NsWindow is null)
            return false;
        //_NsWindow.SetValueForNsobject<bool>("setTitlebarAppearsTransparent:", true);

        _NsWindow.SetValueForNsobject<bool>("setTitlebarAppearsTransparent:", true);
        _NsWindow.SetValueForNsobject<int>("setTitleVisibility:", 0);
        var value = _NsWindow.GetValueFromNsobject<ulong>("styleMask");
        var newValue = value | (ulong)NSWindowStyle.Titled;
        _NsWindow.SetValueForNsobject<ulong>("setStyleMask:", newValue);

        //NSNotification
        //_NsWindow.SetValueForNsobject<bool>("setMovableByWindowBackground:", true);
        return true;
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    bool RemoveTitleBarEx(WindowTitleBarKind titleBar)
    {
        switch (titleBar)
        {
            case WindowTitleBarKind.Default:
                break;
            case WindowTitleBarKind.PlatformDefault:
                break;
            case WindowTitleBarKind.CustomTitleBarAndExtension:
                var vTitleBar = _Window.WindowScene?.Titlebar;
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
        WindowPresenterKind.Default => MoveWindow(_Options.Location, _Options.Size),
        WindowPresenterKind.Maximize => MoveWindowMaximize(),
        WindowPresenterKind.Minimize => MoveWindowMinimize(),
        WindowPresenterKind.FullScreen => ToggleFullScreen(true),
        _ => false,
    };

    bool MoveWindow(WindowAlignment location, Size size)
    {
        if (_NsWindow is null)
            return false;

        var vScreen = _Window.Screen;
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
            case WindowAlignment.LeftTop:
                break;
            case WindowAlignment.RightTop:
                {
                    startX = vCGRect.Width - realWidth;
                    startY = 0;
                }
                break;
            case WindowAlignment.Center:
                {
                    startX = (vCGRect.Width - realWidth) / 2.0;
                    startY = (vCGRect.Height - realHeight) / 2.0;
                }
                break;
            case WindowAlignment.LeftBottom:
                {
                    startX = 0;
                    startY = vCGRect.Height - realHeight;
                }
                break;
            case WindowAlignment.RightBottom:
                {
                    startX = vCGRect.Width - realWidth;
                    startY = vCGRect.Height - realHeight;
                }
                break;
            default:
                break;
        }
        var cgRect = new CGRect(startX, startY, realWidth, realHeight);

        _NsWindow.SetValueForNsobject<CGRect, bool>("setFrame:display:", cgRect, true);

        return true;
    }

    bool MoveWindowMaximize()
    {
        if (_NsWindow is null)
            return false;

        var vScreen = _Window.Screen;
        var vCGRect = vScreen.Bounds;

        double width = vCGRect.Width.Value * _ScalingFactorX;
        double height = vCGRect.Height.Value * _ScalingFactorY;

        var cgRect = new CGRect(0, 0, width, height);
        _NsWindow.SetValueForNsobject<CGRect, bool>("setFrame:display:", cgRect, true);

        return true;
    }

    bool MoveWindowMinimize()
    {
        if (_NsWindow is null)
            return false;

        _NsWindow.SetValueForNsobject<CGRect, bool>("setFrame:display:", new CGRect(0, 0, _Options.Size.Width, _Options.Size.Height), true);
        _NsWindow.ExecuteMethod("center");
        _NsWindow.SetValueForNsobject<IntPtr>("miniaturize:", this.Handle);

        //_NsApplication.SetValueForNsobject<IntPtr>("hide:", _NsWindow.Handle);

        return true;
    }

    bool MoveWindowRestore()
    {
        _NsApplication.SetValueForNsobject<bool>("activateIgnoringOtherApps:", true);
        _NsWindow?.SetValueForNsobject<IntPtr>("makeKeyAndOrderFront:", this.Handle);
        return true;
    }

    bool ToggleFullScreen(bool bFullScreen)
    {
        _NsWindow?.SetValueForNsobject<IntPtr>("toggleFullScreen:", this.Handle);
        return true;
    }
}

