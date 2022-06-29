using Microsoft.Maui.Platform;
using static PInvoke.User32;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkitx.Platforms.Windows.Controllers;

internal partial class WinuiWindowController : IService
{
    public WinuiWindowController(Window window, WindowChrome windowChrome)
    {
        _Window = window;
        _WindowChrome = windowChrome;
        _WinUIWindow = _Window.Handler.PlatformView as MicrosoftuiXaml.Window;
        _AppWindow = _WinUIWindow?.GetAppWindow();

        if (_WinUIWindow?.Content is WindowRootView windowRootView)
            _WindowRootView = windowRootView;
    }

    readonly Window _Window;
    readonly WindowChrome _WindowChrome;

    readonly WindowRootView? _WindowRootView;
    readonly MicrosoftuiXaml.Window? _WinUIWindow;
    readonly MicrosoftuiWindowing.AppWindow? _AppWindow;

    bool IService.Run()
    {
        if (_WindowRootView is null)
            return false;

        ShownInSwitchers(_WindowChrome.ShowInSwitcher);
        ShowInTopMost(_WindowChrome.TopMost);
        MoveWindow(_WindowChrome.WindowAlignment, new Size(_WindowChrome.Width, _WindowChrome.Height));
        ShowPresenter(_WindowChrome.WindowPresenterKind);

        return true;
    }

    bool IService.Stop()
    {
        return true;
    }



}

internal partial class WinuiWindowController
{
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
                MoveWindow(_WindowChrome.WindowAlignment, new Size(_WindowChrome.Width, _WindowChrome.Height));
                break;
        }

        return true;
    }

}

internal partial class WinuiWindowController
{
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

        if (bFullScreen)
        {
            if (_WindowChrome.WindowTitleBarKind is WindowTitleBarKind.Default or WindowTitleBarKind.DefaultWithExtension)
                _WinUIWindow.ExtendsContentIntoTitleBar = false;

            if (_AppWindow.Presenter.Kind is not MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
                _AppWindow.SetPresenter(MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen);
        }
        else
        {
            if (_WindowChrome.WindowTitleBarKind is WindowTitleBarKind.Default or WindowTitleBarKind.DefaultWithExtension)
                _WinUIWindow.ExtendsContentIntoTitleBar = true;

            if (_AppWindow.Presenter.Kind is MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
            {
                var customOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.CreateForContextMenu();
                _AppWindow.SetPresenter(customOverlappedPresenter);
            }
        }

        return true;
    }
}

