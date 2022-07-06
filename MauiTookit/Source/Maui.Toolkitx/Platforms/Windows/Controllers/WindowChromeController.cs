using Microsoft.Maui.Platform;
using static PInvoke.User32;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkitx.Platforms.Windows.Controllers;

internal partial class WindowChromeController : IService
{
    public WindowChromeController(Window window, WindowChrome windowChrome)
    {
        _Window = window;
        _WindowChrome = windowChrome;
        _WinUIWindow = _Window.Handler.PlatformView as MicrosoftuiXaml.Window;
        _AppWindow = _WinUIWindow?.GetAppWindow();
    }

    readonly Window _Window;
    readonly WindowChrome _WindowChrome;

    readonly MicrosoftuiXaml.Window? _WinUIWindow;
    readonly MicrosoftuiWindowing.AppWindow? _AppWindow;

    bool IService.Run()
    {
        LoadAppWindowEvent();
        //ShownInSwitchers(_WindowChrome.ShowInSwitcher);
        //MoveWindow(_WindowChrome.WindowAlignment, new Size(_WindowChrome.Width, _WindowChrome.Height));
        //ShowPresenter(_WindowChrome.WindowPresenterKind);
        //ShowInTopMost(_WindowChrome.TopMost);

        return true;
    }

    bool IService.Stop()
    {
        UnloadAppWindowEvent();
        return true;
    }
}



internal partial class WindowChromeController
{
    bool LoadAppWindowEvent()
    {
        if (_AppWindow is not null)
            _AppWindow.Changed += AppWindow_Changed;

        return true;
    }

    bool UnloadAppWindowEvent()
    {
        if (_AppWindow is not null)
            _AppWindow.Changed -= AppWindow_Changed;

        return true;
    }

    void AppWindow_Changed(MicrosoftuiWindowing.AppWindow sender, MicrosoftuiWindowing.AppWindowChangedEventArgs args)
    {

    }
}

