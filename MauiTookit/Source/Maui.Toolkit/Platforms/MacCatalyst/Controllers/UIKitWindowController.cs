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
        _ChangeOptions = options with { };
        _IsMainWindow = isMainWinodw;
        _NsWindow = uiWindow.GetHostWidnowForUiWindow();
    }

    readonly NSObject _NsApplication;
    readonly UIApplication _Application;
    readonly UIWindow _Window;
    readonly StartupOptions _Options;
    readonly StartupOptions _ChangeOptions;
    readonly bool _IsMainWindow;

    readonly NSObject? _NsWindow;
    readonly double _ScalingFactorX = 1.0d;
    readonly double _ScalingFactorY = 1.0d;

    bool IController.Run()
    {
        LoadBackgroundMaterial(_Options.BackdropsKind);
        MoveWindow(_Options.PresenterKind);
        RemoveTitleBar(_Options.TitleBarKind);
        LoadButton(_Options.ConfigurationKind);
        return true;
    }

    bool IController.Stop()
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SetBackdrop(BackdropsKind kind)
    {
        _ChangeOptions.BackdropsKind = kind;
        return true;
    }

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind)
    {
        _ChangeOptions.TitleBarKind = kind;
        RemoveTitleBar(kind);
        return LoadButton(_Options.ConfigurationKind);
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
        if (_NsWindow is null)
            return false;

        _NsWindow.SetValueForNsobject<NFloat>("setAlphaValue:", new NFloat(1));

        return true;
    }

    bool RemoveTitleBar(WindowTitleBarKind titleBar)
    {
        if (_NsWindow is null)
            return false;

        NSWindowStyle styleMask = NSWindowStyle.Borderless;
        _NsWindow.SetValueForNsobject<long>("setTitleVisibility:", (long)TitlebarTitleVisibility.Visible);
        _NsWindow.SetValueForNsobject<bool>("setMovableByWindowBackground:", true);

        switch (titleBar)
        {
            case WindowTitleBarKind.Default:
                {
                    styleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable | NSWindowStyle.FullSizeContentView;
                    _NsWindow.SetValueForNsobject<bool>("setTitlebarAppearsTransparent:", true);
                }
                break;
            case WindowTitleBarKind.DefaultWithExtension:
                {
                    styleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable;
                    _NsWindow.SetValueForNsobject<bool>("setTitlebarAppearsTransparent:", true);
                }
                break;
            case WindowTitleBarKind.CustomTitleBarAndExtension:
                {
                    styleMask = NSWindowStyle.Titled & NSWindowStyle.Closable & NSWindowStyle.Miniaturizable & NSWindowStyle.Resizable & NSWindowStyle.FullSizeContentView;
                }
                break;
            default:
                {
                    styleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable | NSWindowStyle.FullSizeContentView;
                    _NsWindow.SetValueForNsobject<bool>("setTitlebarAppearsTransparent:", false);
                }
                break;
        }
        _NsWindow.SetValueForNsobject<ulong>("setStyleMask:", (ulong)styleMask);
        return true;
    }

    bool LoadButton(WindowConfigurationKind kind)
    {
        if (_NsWindow is null)
            return false;

        var styleMask = (NSWindowStyle)_NsWindow.GetValueFromNsobject<ulong>("styleMask");
        switch (kind)
        {
            case WindowConfigurationKind.ShowAllButton:
                styleMask = styleMask | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable;
                break;
            case WindowConfigurationKind.HideAllButton:
                styleMask = styleMask & NSWindowStyle.Closable & NSWindowStyle.Miniaturizable & NSWindowStyle.Resizable;
                break;
            default:
                {
                    styleMask = styleMask | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable;

                    var isDislbaleMin = kind.HasFlag(WindowConfigurationKind.DisableMinizable);
                    if (isDislbaleMin)
                        styleMask = styleMask & NSWindowStyle.Miniaturizable;

                    var isDisableResie = kind.HasFlag(WindowConfigurationKind.DisableResizable);
                    if (isDisableResie)
                        styleMask = styleMask & NSWindowStyle.Resizable;

                    var isDisableClose = kind.HasFlag(WindowConfigurationKind.DisableClosable);
                    if (isDisableClose)
                        styleMask = styleMask & NSWindowStyle.Closable;
                }
                break;
        }

        _NsWindow.SetValueForNsobject<ulong>("setStyleMask:", (ulong)styleMask);
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
        _NsWindow.ExecuteMethod("center");

        return true;
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
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
        _NsWindow.ExecuteMethod("center");

        var rootView = _Window.RootViewController;
        if (rootView is not null)
            rootView.WantsFullScreenLayout = true;

        return true;
    }

    bool MoveWindowMinimize()
    {
        if (_NsWindow is null)
            return false;

        if (_ChangeOptions.TitleBarKind is WindowTitleBarKind.CustomTitleBarAndExtension)
            _NsApplication.SetValueForNsobject<IntPtr>("hide:", _NsWindow.Handle);
        else
            _NsWindow.SetValueForNsobject<IntPtr>("miniaturize:", this.Handle);

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
        if (_NsWindow is null)
            return false;

        _NsWindow.SetValueForNsobject<IntPtr>("toggleFullScreen:", this.Handle);
        var styleMask = (NSWindowStyle)_NsWindow.GetValueFromNsobject<ulong>("styleMask");
        if (styleMask.HasFlag(NSWindowStyle.FullScreenWindow))
        {

        }



        return true;
    }
}
