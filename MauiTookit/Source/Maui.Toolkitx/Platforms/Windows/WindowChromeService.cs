using Microsoft.Maui.Platform;
using System.Reflection;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using Microsoftui = Microsoft.UI;
using Winui = Windows.UI;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using Windowsgraphics = Windows.Graphics;
using MicrosoftuixamlData = Microsoft.UI.Xaml.Data;
using Maui.Toolkitx.Extensions;
using Microsoft.Maui.Controls.Platform;
using Maui.Toolkitx.Platforms.Windows.Extensions;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowChromeService : IService
{
    public WindowChromeService(Window window, WindowChrome windowChrome)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(windowChrome);

        _Application = MicrosoftuiXaml.Application.Current;

        _Window = window;
        _WindowChrome = windowChrome;

        _WinUIWindow = _Window.Handler.PlatformView as MicrosoftuiXaml.Window;
        _AppWindow = _WinUIWindow?.GetAppWindow();
        //WindowRootViewContainer
        var panel = _WinUIWindow?.Content as MicrosoftuixamlControls.Panel;
        var mauiContext = _Window.Page?.RequireMauiContext();
        if (mauiContext is not null)
        {
           var platformElement = window.Page?.ToPlatform() as ShellView;
            _RootNavigationView = platformElement;

            _WindowRootView = mauiContext.GetNavigationRootManager().RootView as WindowRootView;
        }

        //if (_WinUIWindow?.Content is WindowRootView windowRootView)
        //_WindowRootView = windowRootView;
    }

    readonly MicrosoftuiXaml.Application _Application;
    readonly Window _Window;
    readonly WindowChrome _WindowChrome;

    readonly WindowRootView? _WindowRootView;
    readonly MicrosoftuiXaml.Window? _WinUIWindow;
    readonly MicrosoftuiWindowing.AppWindow? _AppWindow;

    readonly RootNavigationView? _RootNavigationView;
    MicrosoftuiXaml.FrameworkElement? _TitleBar;

    bool _IsTitleBarIsSet = false;

    bool _IsLastFullScreen = false;

    bool IService.Run()
    {
        //if (_WindowRootView is null)
            //return false;

        if (_AppWindow is not null)
            _AppWindow.Changed += AppWindow_Changed;

        //_RootNavigationView = _WindowRootView.NavigationViewControl;
        LoadTitleBarColor(_WindowChrome.CaptionActiveBackgroundColor, _WindowChrome.CaptionInactiveBackgroundColor, _WindowChrome.CaptionActiveForegroundColor, _WindowChrome.CaptionInactiveForegroundColor);
        LoadWindowRootViewEvent();
        LoadWindowEvent();
        RegisterApplicationThemeChangedEvent();

        return true;
    }

    bool IService.Stop()
    {
        if (_AppWindow is not null)
            _AppWindow.Changed += AppWindow_Changed;

        UnloadWindowRootViewEvent();
        UnloadWindowEvent();
        UnregisterApplicationThemeChangedEvent();

        return true;
    }

}
