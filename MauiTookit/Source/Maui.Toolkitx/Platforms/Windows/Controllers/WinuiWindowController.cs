using Microsoft.Maui.Platform;
using PInvoke;
using WinRT;
using static PInvoke.User32;
using Microsoftui = Microsoft.UI;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using Windowsgraphics = Windows.Graphics;
using MicrosoftuixamlData = Microsoft.UI.Xaml.Data;
using Winui = Windows.UI;
using System.Reflection;

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

    RootNavigationView? _RootNavigationView = default;
    MicrosoftuiXaml.FrameworkElement? _TitleBar;

    bool IService.Run()
    {
        if (_WindowRootView is null)
            return false;

        _RootNavigationView = _WindowRootView.NavigationViewControl;
        LoadWindowRootViewEvent();

        return true;
    }

    bool IService.Stop()
    {


        return true;
    }

    bool LoadWindowRootViewEvent()
    {
        if (_WindowRootView is null)
            return false;

        var titlBarEventHandle = typeof(WindowRootView).GetEvent("OnAppTitleBarChanged", BindingFlags.Instance | BindingFlags.NonPublic);
        if (titlBarEventHandle is not null)
        {
            var addMethod = titlBarEventHandle.AddMethod;
            addMethod?.Invoke(_WindowRootView, new object[] { new EventHandler(OnAppTitleBarChanged) });
        }

        var contentEventHandle = typeof(WindowRootView).GetEvent("ContentChanged", BindingFlags.Instance | BindingFlags.NonPublic);
        if (contentEventHandle is not null)
        {
            var addMethod = contentEventHandle.AddMethod;
            addMethod?.Invoke(_WindowRootView, new object[] { new EventHandler(OnContentChanged) });
        }

        return true;
    }

}

internal partial class WinuiWindowController
{
    void OnAppTitleBarChanged(object? sender, EventArgs e)
    {

    }

    void OnContentChanged(object? sender, EventArgs e)
    {

    }

}


