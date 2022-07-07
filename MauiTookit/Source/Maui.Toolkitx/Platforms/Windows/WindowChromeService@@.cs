using Maui.Toolkitx.Platforms.Windows.Extensions;
using Maui.Toolkitx.Platforms.Windows.Runtimes.User32;
using Microsoft.Maui.Platform;
using PInvoke;
using WinRT;
using static PInvoke.User32;
using Microsoftui = Microsoft.UI;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using MicrosoftuixamlData = Microsoft.UI.Xaml.Data;
using MicrosoftuixamlmediaImaging = Microsoft.UI.Xaml.Media.Imaging;
using Winui = Windows.UI;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowChromeService
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

    bool LoadTitleBarColor(Color? background, Color? inactiveBackground, Color? foreground, Color? inactiveForeground)
    {
        var resource = _Application.Resources;
        if (background != null)
            resource["WindowCaptionBackground"] = background.ToPlatform();

        if (inactiveBackground != null)
            resource["WindowCaptionBackgroundDisabled"] = inactiveBackground.ToPlatform();

        if (foreground != null)
            resource["WindowCaptionForeground"] = foreground.ToPlatform();

        if (inactiveForeground != null)
            resource["WindowCaptionForegroundDisabled"] = inactiveForeground.ToPlatform();

        //resource["TitleBarHeight"] = 50;


        TriggertTitleBarRepaint();

        return true;
    }

    bool LoadAppTitleBar(double height, double fontSize)
    {
        var propertyInfo = typeof(WindowRootView).GetProperty("AppTitleBar", BindingFlags.Instance | BindingFlags.NonPublic);
        var titleBar = propertyInfo?.GetValue(_WindowRootView);
        if (titleBar is not MicrosoftuiXaml.FrameworkElement frameworkElement)
            return false;

        if (height > 0)
            frameworkElement.Height = height;

        if (fontSize > 0)
        {
            var textBlock = frameworkElement.GetFirstDescendant<MicrosoftuixamlControls.TextBlock>();
            if (textBlock is not null)
                textBlock.FontSize = fontSize;
        }

        return true;
    }

    bool LoadAppIcon(string? icon)
    {
        if (string.IsNullOrWhiteSpace(icon))
            return false;

        var propertyInfo1 = typeof(WindowRootView).GetProperty("AppFontIcon", BindingFlags.Instance | BindingFlags.NonPublic);
        if (propertyInfo1?.GetValue(_WindowRootView) is MicrosoftuixamlControls.Image image)
        {
            Uri imageUri = new(icon, UriKind.RelativeOrAbsolute);
            MicrosoftuixamlmediaImaging.BitmapImage imageBitmap = new(imageUri);
            image.Source = imageBitmap;
            image.Width = 25;
            image.Height = 25;
        }

        return true;
    }

    bool SetButtonConfigrations(WindowButtonKind kind)
    {
        if (_AppWindow is null)
            return false;

        switch (kind)
        {
            case WindowButtonKind.Hide:
                var customOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.CreateForContextMenu();
                _AppWindow.SetPresenter(customOverlappedPresenter);
                break;
            case WindowButtonKind.Show:
                var mainOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.Create();
                _AppWindow.SetPresenter(mainOverlappedPresenter);
                break;
            default:
                {
                    if (_AppWindow.Presenter.Kind is not MicrosoftuiWindowing.AppWindowPresenterKind.Overlapped)
                        _AppWindow.SetPresenter(MicrosoftuiWindowing.AppWindowPresenterKind.Overlapped);

                    var overlappedPresenter = _AppWindow.Presenter.As<MicrosoftuiWindowing.OverlappedPresenter>();
                    if (overlappedPresenter is not null)
                    {
                        overlappedPresenter.IsMinimizable = kind.HasFlag(WindowButtonKind.EnableMinizable);
                        overlappedPresenter.IsMaximizable = kind.HasFlag(WindowButtonKind.EnableMaximizable);
                        overlappedPresenter.IsResizable = kind.HasFlag(WindowButtonKind.EnableResizable);
                    }
                }
                break;
        }

        return true;
    }

    bool RestorButtonConfigrations()
    {
        if (_AppWindow is null)
            return false;

        var overlappedPresenter = _AppWindow.Presenter.As<MicrosoftuiWindowing.OverlappedPresenter>();
        if (overlappedPresenter is not null)
        {
            overlappedPresenter.IsMinimizable = true;
            overlappedPresenter.IsMaximizable = true;
            overlappedPresenter.IsResizable = true;
        }
        else
        {
            var mainOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.Create();
            _AppWindow.SetPresenter(mainOverlappedPresenter);
        }

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

    bool TriggertTitleBarRepaint()
    {
        if (_WinUIWindow is null)
            return false;

        var windowHanlde = _WinUIWindow.GetWindowHandle();
        var activeWindow = User32.GetActiveWindow();
        if (windowHanlde == activeWindow)
        {
            User32.PostMessage(windowHanlde, WindowMessage.WM_ACTIVATE, new IntPtr((int)User32Constants.WA_INACTIVE), IntPtr.Zero);
            User32.PostMessage(windowHanlde, WindowMessage.WM_ACTIVATE, new IntPtr((int)User32Constants.WA_ACTIVE), IntPtr.Zero);
        }
        else
        {
            User32.PostMessage(windowHanlde, WindowMessage.WM_ACTIVATE, new IntPtr((int)User32Constants.WA_ACTIVE), IntPtr.Zero);
            User32.PostMessage(windowHanlde, WindowMessage.WM_ACTIVATE, new IntPtr((int)User32Constants.WA_INACTIVE), IntPtr.Zero);
        }

        return true;
    }

    void WindowRootView_Loaded(object sender, MicrosoftuiXaml.RoutedEventArgs e)
    {
        var propertyInfo = typeof(WindowRootView).GetProperty("AppTitleBar", BindingFlags.Instance | BindingFlags.NonPublic);
        var titleBar = propertyInfo?.GetValue(_WindowRootView);
        if (titleBar is MicrosoftuiXaml.FrameworkElement frameworkElement)
            _TitleBar = frameworkElement;

        LoadAppTitleBar(_WindowChrome.CaptionHeight, _WindowChrome.TitleFontSize);
        LoadAppIcon(_WindowChrome.Icon);
        SwitchTitleBar(_WindowChrome.WindowTitleBarKind);
        if (_WindowChrome.WindowTitleBarKind is not WindowTitleBarKind.Default or WindowTitleBarKind.DefaultWithExtension)
            SetButtonConfigrations(_WindowChrome.WindowButtonKind);

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

    void OnAppTitleBarChanged(object? sender, EventArgs e)
    {

    }

    void OnContentChanged(object? sender, EventArgs e)
    {

    }

    void WindowRootView_SizeChanged(object sender, MicrosoftuiXaml.SizeChangedEventArgs e)
    {

    }

    void Application_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        LoadTitleBarCorlor(_AppWindow?.TitleBar);
    }



    private void AppWindow_Changed(MicrosoftuiWindowing.AppWindow sender, MicrosoftuiWindowing.AppWindowChangedEventArgs args)
    {
        if (!args.DidPresenterChange)
            return;

        if (_IsLastFullScreen)
            SetButtonConfigrations(_WindowChrome.WindowButtonKind);

        if (sender.Presenter.Kind == MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
            _IsLastFullScreen = true;
        else
            _IsLastFullScreen = false;

    }


}
