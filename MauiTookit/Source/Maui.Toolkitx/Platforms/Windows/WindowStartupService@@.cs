﻿using Maui.Toolkitx.Config;
using Maui.Toolkitx.Platforms.Windows.Controllers;
using Microsoft.Maui.Platform;
using static PInvoke.User32;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowStartupService
{
    bool SwitchBackdrop(BackdropsKind kind, BackdropConfigurations config)
    {
        _BackdropService?.Stop();
        _BackdropService = default;

        switch (kind)
        {
            case BackdropsKind.Default:
                break;
            case BackdropsKind.Mica:
                _BackdropService = new WinuiMicaController(_Window, config);
                break;
            case BackdropsKind.Acrylic:
                _BackdropService = new WinuiAcrylicController(_Window, config);
                break;
            case BackdropsKind.BlurEffect:
                break;
            default:
                break;
        }
        _BackdropService?.Run();
        return true;
    }

    bool ShownInSwitchers(bool isShownInSwitchers)
    {
        if (_AppWindow is null)
            return false;

        _AppWindow.IsShownInSwitchers = isShownInSwitchers;
        return true;
    }

    bool ShowInTopMost(bool isTopMost)
    {
        if (_AppWindow is null)
            return false;

        _AppWindow.MoveInZOrderAtTop();
        //_AppWindow.ShowOnceWithRequestedStartupState();
        return true;
    }

    bool ShowPresenter(WindowPresenterKind kind)
    {
        switch (kind)
        {
            case WindowPresenterKind.Maximize:
                MaximizeWidnow();
                break;
            case WindowPresenterKind.Minimize:
                MiniaturizeWindow();
                break;
            case WindowPresenterKind.FullScreen:
                ToggleFullScreen(true);
                break;
            default:
                ToggleFullScreen(false);
                //MoveWindow(_WindowStartup.WindowAlignment, new Size(_WindowStartup.Width, _WindowStartup.Height));
                break;
        }

        return true;
    }

    bool MoveWindow(WindowAlignment alignment, Size size)
    {
        if (_WinUIWindow is null)
            return false;

        if (_AppWindow is null)
            return false;

        var displyArea = MicrosoftuiWindowing.DisplayArea.Primary;
        double scalingFactor = _WinUIWindow.GetDisplayDensity();

        var width = size.Width * scalingFactor;
        var height = size.Height * scalingFactor;

        if (width > displyArea.WorkArea.Width)
            width = displyArea.WorkArea.Width;

        if (height > displyArea.WorkArea.Height)
            height = displyArea.WorkArea.Height;

        double startX = 0;
        double startY = 0;

        switch (alignment)
        {
            case WindowAlignment.LeftTop:
                break;
            case WindowAlignment.RightTop:
                startX = (displyArea.WorkArea.Width - width);
                break;
            case WindowAlignment.Center:
                startX = (displyArea.WorkArea.Width - width) / 2.0;
                startY = (displyArea.WorkArea.Height - height) / 2.0;
                break;
            case WindowAlignment.LeftBottom:
                startY = (displyArea.WorkArea.Height - height);
                break;
            case WindowAlignment.RightBottom:
                startX = (displyArea.WorkArea.Width - width);
                startY = (displyArea.WorkArea.Height - height);
                break;
            default:
                break;
        }

        _AppWindow.MoveAndResize(new((int)startX, (int)startY, (int)width, (int)height), displyArea);
        return true;
    }

    bool MiniaturizeWindow()
    {
        if (_WinUIWindow is null)
            return false;

        var windowHanlde = _WinUIWindow.GetWindowHandle();
        PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_MINIMIZE), IntPtr.Zero);

        return true;
    }

    bool MaximizeWidnow()
    {
        if (_WinUIWindow is null)
            return false;

        var windowHanlde = _WinUIWindow.GetWindowHandle();
        PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_MAXIMIZE), IntPtr.Zero);

        return true;
    }

    bool ToggleFullScreen(bool bFullScreen)
    {
        if (_WinUIWindow is null)
            return false;

        if (_AppWindow is null)
            return false;

        var windowChrome = WindowChrome.GetWindowChrome(_Window);

        if (bFullScreen)
        {
            if (windowChrome is not null)
            {
                if (windowChrome.WindowTitleBarKind is WindowTitleBarKind.Default or WindowTitleBarKind.DefaultWithExtension)
                    _WinUIWindow.ExtendsContentIntoTitleBar = false;
            }


            if (_AppWindow.Presenter.Kind is not MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
                _AppWindow.SetPresenter(MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen);
        }
        else
        {
            if (windowChrome is not null)
            {
                if (windowChrome.WindowTitleBarKind is WindowTitleBarKind.Default or WindowTitleBarKind.DefaultWithExtension)
                    _WinUIWindow.ExtendsContentIntoTitleBar = true;
            }

            if (_AppWindow.Presenter.Kind is MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
            {
                var customOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.CreateForContextMenu();
                _AppWindow.SetPresenter(customOverlappedPresenter);
            }
        }

        return true;
    }


}
