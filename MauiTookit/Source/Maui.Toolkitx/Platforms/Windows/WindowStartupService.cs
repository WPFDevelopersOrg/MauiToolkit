using Microsoft.Maui.Platform;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowStartupService : IWindowStartupService
{
    public WindowStartupService(Window window, WindowStartup windowStartup)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(windowStartup);
        _Window = window;
        _WindowStartup = windowStartup;
        _WinUIWindow = _Window.Handler.PlatformView as MicrosoftuiXaml.Window;
        _AppWindow = _WinUIWindow?.GetAppWindow();
    }

    readonly Window _Window;
    readonly WindowStartup _WindowStartup;

    readonly MicrosoftuiXaml.Window? _WinUIWindow;
    readonly MicrosoftuiWindowing.AppWindow? _AppWindow;

    IService? _BackdropService;
    bool IService.Run()
    {
        SwitchBackdrop(_WindowStartup.BackdropsKind, _WindowStartup.BackdropConfigurations);
        ShownInSwitchers(_WindowStartup.ShowInSwitcher);
        MoveWindow(_WindowStartup.WindowAlignment, new Size(_WindowStartup.Width, _WindowStartup.Height));
        ShowPresenter(_WindowStartup.WindowPresenterKind);
        ShowInTopMost(_WindowStartup.TopMost);

        return true;
    }

    bool IService.Stop()
    {
        _BackdropService?.Stop();

        return true;
    }


}
