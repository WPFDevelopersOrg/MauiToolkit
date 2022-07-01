using Microsoft.Maui.Platform;
using System.Reflection;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using Microsoftui = Microsoft.UI;
using Winui = Windows.UI;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using Windowsgraphics = Windows.Graphics;
using MicrosoftuixamlData = Microsoft.UI.Xaml.Data;

namespace Maui.Toolkitx.Platforms.Windows.Controllers;

internal partial class WinuiTitleBarController : IService
{
    public WinuiTitleBarController(Window window, WindowChrome windowChrome)
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
    bool _IsTitleBarIsSet = false;

    bool IService.Run()
    {
        if (_WindowRootView is null)
            return false;

        _RootNavigationView = _WindowRootView.NavigationViewControl;
        LoadWindowRootViewEvent();
        LoadWindowEvent();
        RegisterApplicationThemeChangedEvent();

        return true;
    }

    bool IService.Stop()
    {
        UnloadWindowRootViewEvent();
        UnloadWindowEvent();
        UnregisterApplicationThemeChangedEvent();
        return true;
    }
}

internal partial class WinuiTitleBarController
{
    bool SwitchTitleBar(WindowTitleBarKind titleBar)
    {
        if (_WinUIWindow is null)
            return false;

        if (_AppWindow is null)
            return false;

        if (_IsTitleBarIsSet)
            _AppWindow.TitleBar?.ResetToDefault();

        switch (titleBar)
        {
            case WindowTitleBarKind.PlatformDefault:
                _WinUIWindow.ExtendsContentIntoTitleBar = false;
                if (_TitleBar is not null)
                    _TitleBar.Visibility = MicrosoftuiXaml.Visibility.Collapsed;
                break;
            case WindowTitleBarKind.CustomTitleBarAndExtension:

                _WinUIWindow.ExtendsContentIntoTitleBar = false;
                if (_TitleBar is not null)
                    _TitleBar.Visibility = MicrosoftuiXaml.Visibility.Collapsed;
                LoadTitleBarCorlor(_AppWindow.TitleBar);

                var thicknessProperty = typeof(MauiNavigationView).GetProperty("NavigationViewContentMargin", BindingFlags.Instance | BindingFlags.NonPublic);
                if (thicknessProperty?.GetValue(_RootNavigationView) is MicrosoftuiXaml.Thickness thickness)
                    thicknessProperty.SetValue(_RootNavigationView, new MicrosoftuiXaml.Thickness(0));

                if (!MicrosoftuiWindowing.AppWindowTitleBar.IsCustomizationSupported())
                    break;

                if (_AppWindow.TitleBar is null)
                    break;

                _AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                _AppWindow.TitleBar.PreferredHeightOption = MicrosoftuiWindowing.TitleBarHeightOption.Standard;
                _AppWindow.TitleBar.IconShowOptions = MicrosoftuiWindowing.IconShowOptions.HideIconAndSystemMenu;

                break;
            default:
                _WinUIWindow.ExtendsContentIntoTitleBar = true;
                if (_TitleBar is not null)
                    _TitleBar.Visibility = MicrosoftuiXaml.Visibility.Visible;
                break;
        }

        _IsTitleBarIsSet = true;
        return true;
    }

    bool LoadTitleBarCorlor(MicrosoftuiWindowing.AppWindowTitleBar? titleBar)
    {
        if (titleBar is null)
            return false;

        var application = Application.Current;
        if (application is null)
            return false;

        AppTheme? theme = application.PlatformAppTheme;
        if (theme is null)
            return false;

        switch (application.UserAppTheme)
        {
            case AppTheme.Light:
            case AppTheme.Dark:
                theme = application.UserAppTheme;
                break;
            default:
                break;
        }

        switch (theme)
        {
            case AppTheme.Dark:
                // Set active window colors
                titleBar.ForegroundColor = Microsoftui.Colors.White;
                titleBar.BackgroundColor = Microsoftui.Colors.Red;
                titleBar.ButtonForegroundColor = Microsoftui.Colors.White;
                titleBar.ButtonBackgroundColor = Microsoftui.Colors.Transparent;

                titleBar.ButtonHoverForegroundColor = Microsoftui.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Microsoftui.Colors.BlueViolet;

                titleBar.ButtonPressedForegroundColor = Winui.Color.FromArgb(80, 255, 255, 255);
                titleBar.ButtonPressedBackgroundColor = Microsoftui.Colors.DarkSeaGreen;

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Microsoftui.Colors.White;
                titleBar.InactiveBackgroundColor = null;

                titleBar.ButtonInactiveForegroundColor = Microsoftui.Colors.White;
                titleBar.ButtonInactiveBackgroundColor = Microsoftui.Colors.Transparent;
                break;
            default:
                // Set active window colors
                titleBar.ForegroundColor = Microsoftui.Colors.Black;
                titleBar.BackgroundColor = null;
                titleBar.ButtonForegroundColor = Microsoftui.Colors.Gray;
                titleBar.ButtonBackgroundColor = Microsoftui.Colors.Transparent;

                titleBar.ButtonHoverForegroundColor = Microsoftui.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Microsoftui.Colors.BlueViolet;

                titleBar.ButtonPressedForegroundColor = Winui.Color.FromArgb(80, 255, 255, 255);
                titleBar.ButtonPressedBackgroundColor = Microsoftui.Colors.BlueViolet;

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Microsoftui.Colors.Gainsboro;
                titleBar.InactiveBackgroundColor = null;
                titleBar.ButtonInactiveForegroundColor = Microsoftui.Colors.AliceBlue;
                titleBar.ButtonInactiveBackgroundColor = Microsoftui.Colors.Transparent;
                break;
        }

        //titleBar.SetDragRectangles

        return true;
    }

}

internal partial class WinuiTitleBarController
{
    private MicrosoftuiXaml.Thickness _NavigationViewContentMargin;
    public MicrosoftuiXaml.Thickness NavigationViewContentMargin
    {
        get => _NavigationViewContentMargin;
        set
        {
            _NavigationViewContentMargin = value;
            if (_WindowChrome.WindowTitleBarKind is WindowTitleBarKind.CustomTitleBarAndExtension)
            {
                var thicknessProperty = typeof(MauiNavigationView).GetProperty("NavigationViewContentMargin", BindingFlags.Instance | BindingFlags.NonPublic);
                if (thicknessProperty?.GetValue(_RootNavigationView) is MicrosoftuiXaml.Thickness thickness)
                {
                    if (thickness == new MicrosoftuiXaml.Thickness(0))
                        return;

                    thicknessProperty.SetValue(_RootNavigationView, new MicrosoftuiXaml.Thickness(0));
                }
            }
        }
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

    bool UnloadWindowRootViewEvent()
    {
        if (_WindowRootView is null)
            return false;

        var titlBarEventHandle = typeof(WindowRootView).GetEvent("OnAppTitleBarChanged", BindingFlags.Instance | BindingFlags.NonPublic);
        if (titlBarEventHandle is not null)
        {
            var removeMethod = titlBarEventHandle.RemoveMethod;
            removeMethod?.Invoke(_WindowRootView, new object[] { new EventHandler(OnAppTitleBarChanged) });
        }

        var contentEventHandle = typeof(WindowRootView).GetEvent("ContentChanged", BindingFlags.Instance | BindingFlags.NonPublic);
        if (contentEventHandle is not null)
        {
            var removeMethod = contentEventHandle.RemoveMethod;
            removeMethod?.Invoke(_WindowRootView, new object[] { new EventHandler(OnContentChanged) });
        }

        return true;
    }

    bool LoadWindowEvent()
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.Loaded += WindowRootView_Loaded;
        _RootNavigationView.Unloaded += WindowRootView_Unloaded;
        _RootNavigationView.SizeChanged += WindowRootView_SizeChanged;

        return true;
    }

    bool UnloadWindowEvent()
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.Loaded -= WindowRootView_Loaded;
        _RootNavigationView.Unloaded -= WindowRootView_Unloaded;
        _RootNavigationView.SizeChanged -= WindowRootView_SizeChanged;
        return true;
    }

    bool RegisterApplicationThemeChangedEvent()
    {
        var application = Application.Current;
        if (application is null)
            return false;

        application.RequestedThemeChanged += Application_RequestedThemeChanged;
        return true;
    }

    bool UnregisterApplicationThemeChangedEvent()
    {
        var application = Application.Current;
        if (application is null)
            return false;

        application.RequestedThemeChanged -= Application_RequestedThemeChanged;
        return true;
    }

    void OnAppTitleBarChanged(object? sender, EventArgs e)
    {

    }

    void OnContentChanged(object? sender, EventArgs e)
    {

    }

    void WindowRootView_SizeChanged(object sender, MicrosoftuiXaml.SizeChangedEventArgs e)
    {

    }

    void WindowRootView_Loaded(object sender, MicrosoftuiXaml.RoutedEventArgs e)
    {
        var propertyInfo = typeof(WindowRootView).GetProperty("AppTitleBar", BindingFlags.Instance | BindingFlags.NonPublic);
        var titleBar = propertyInfo?.GetValue(_WindowRootView);
        if (titleBar is MicrosoftuiXaml.FrameworkElement frameworkElement)
            _TitleBar = frameworkElement;

        SwitchTitleBar(_WindowChrome.WindowTitleBarKind);
        //if (_WindowChrome.WindowTitleBarKind  is not WindowTitleBarKind.Default)
            //SetWindowConfigrations(_WindowChrome.ConfigurationKind);

        var contentProperty = typeof(MauiNavigationView).GetProperty("ContentGrid", BindingFlags.Instance | BindingFlags.NonPublic);
        if (contentProperty is not null)
        {
            var contentGrid = contentProperty.GetValue(_RootNavigationView);
            if (contentGrid is MicrosoftuixamlControls.Grid grid)
            {
                MicrosoftuixamlData.Binding marginBinding = new();
                marginBinding.Source = this;
                marginBinding.Path = new MicrosoftuiXaml.PropertyPath("NavigationViewContentMargin");
                marginBinding.Mode = MicrosoftuixamlData.BindingMode.TwoWay;
                marginBinding.UpdateSourceTrigger = MicrosoftuixamlData.UpdateSourceTrigger.PropertyChanged;
                MicrosoftuixamlData.BindingOperations.SetBinding(grid, MicrosoftuixamlControls.Grid.MarginProperty, marginBinding);

            }

        }
    }

    void WindowRootView_Unloaded(object sender, MicrosoftuiXaml.RoutedEventArgs e)
    {

    }

    void Application_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        LoadTitleBarCorlor(_AppWindow?.TitleBar);
    }

}