using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using Microsoft.Maui.Platform;
using WinuiControls = Microsoft.UI.Xaml.Controls;
using PInvoke;
using Windows.Graphics;
using static PInvoke.User32;
using Windows_Graphics = Windows.Graphics;
using Winui = Windows.UI;
using WinuiMedia = Microsoft.UI.Xaml.Media;
using Maui.Toolkit.ExtraDependents;
using Maui.Toolkit.Extensions;
using Maui.Toolkit.Platforms.Windows.Extensions;

namespace Maui.Toolkit.Platforms;

internal class WindowsServiceImp : IWindowsService
{
    public WindowsServiceImp(StartupOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StartupOptions = options;
    }

    readonly StartupOptions _StartupOptions;

    Microsoft.UI.Xaml.Application? _Application;
    Microsoft.UI.Xaml.Window? _MainWindow;
    Microsoft.UI.Windowing.AppWindow? _AppWindow;

    bool _IsLoaded = false;

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        ArgumentNullException.ThrowIfNull(lifecycleBuilder, nameof(lifecycleBuilder));

        lifecycleBuilder.AddWindows(windowsLeftCycle =>
        {
            windowsLeftCycle.OnWindowCreated(window =>
            {
                _MainWindow = window;
                var appWindow = _MainWindow.GetAppWindow();
                if (appWindow is null)
                    return;
                _AppWindow = appWindow;

                RemoveTitleBar(_StartupOptions.TitleBarKind);
                MoveWindow(_StartupOptions.PresenterKind);
                RegisterApplicationThemeChangedEvent();

                var mainPage = Application.Current?.MainPage;
                if (mainPage is not null)
                {
                    mainPage.Loaded += MainPage_Loaded;
                    mainPage.SizeChanged += MainPage_SizeChanged;
                }

            }).OnVisibilityChanged((window, arg) =>
            {

            }).OnActivated((window, arg) =>
            {
                //
            }).OnLaunching((application, arg) =>
            {
                _Application = application;
            }).OnLaunched((application, arg) =>
            {

            }).OnPlatformMessage((w, arg) =>
            {

            }).OnResumed(window =>
            {

            }).OnClosed((window, arg) =>
            {

            });
        });

        return true;
    }

    private void MainPage_SizeChanged(object? sender, EventArgs e)
    {
        if (!_IsLoaded)
            return;

        var rects = LoadRects();
        if (rects == null)
            return;

        SetDragRectangles(rects);
    }

    private void MainPage_Loaded(object? sender, EventArgs e)
    {
        TrySetDragRectangles();

        //if (_MainWindow is null)
        //    return;

        //_MainWindow.ExtendsContentIntoTitleBar = true;
        //WinuiControls.Grid grid = new WinuiControls.Grid()
        //{
        //    Background = new WinuiMedia.SolidColorBrush(Winui.Color.FromArgb(255, 255, 0, 0)),

        //    Height = 100,

        //};

        //_MainWindow.SetTitleBar(default);
    }

    bool RegisterApplicationThemeChangedEvent()
    {
        var application = Application.Current;
        if (application is null)
            return false;

        application.RequestedThemeChanged += (sender, arg) => LoadTitleBarCorlor(_AppWindow?.TitleBar);

        return true;
    }

    bool RemoveTitleBar(WindowTitleBarKind titleBar)
    {
        if (_MainWindow is null)
            return false;

        if (_AppWindow is null)
            return false;

        switch (titleBar)
        {
            case WindowTitleBarKind.PlatformDefault:
                _MainWindow.ExtendsContentIntoTitleBar = false;
                break;
            case WindowTitleBarKind.ExtendsContentIntoTitleBar:
                if (!Microsoft.UI.Windowing.AppWindowTitleBar.IsCustomizationSupported())
                    break;

                _MainWindow.ExtendsContentIntoTitleBar = false;
                _AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                _AppWindow.TitleBar.IconShowOptions = Microsoft.UI.Windowing.IconShowOptions.HideIconAndSystemMenu;
                LoadTitleBarCorlor(_AppWindow.TitleBar);
                break;
            default:
                break;
        }

        return true;
    }

    bool LoadTitleBarCorlor(Microsoft.UI.Windowing.AppWindowTitleBar? titleBar)
    {
        if (titleBar is null)
            return false;

        if (_StartupOptions.TitleBarKind is not WindowTitleBarKind.ExtendsContentIntoTitleBar)
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
                titleBar.ForegroundColor = Microsoft.UI.Colors.White;
                titleBar.BackgroundColor = null;
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;

                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Microsoft.UI.Colors.BlueViolet;

                titleBar.ButtonPressedForegroundColor = Winui.Color.FromArgb(80, 255, 255, 255);
                titleBar.ButtonPressedBackgroundColor = Microsoft.UI.Colors.DarkSeaGreen;

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Microsoft.UI.Colors.White;
                titleBar.InactiveBackgroundColor = null;

                titleBar.ButtonInactiveForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
                break;
            default:
                // Set active window colors
                titleBar.ForegroundColor = Microsoft.UI.Colors.Black;
                titleBar.BackgroundColor = null;
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.Gray;
                titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;

                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Microsoft.UI.Colors.BlueViolet;

                titleBar.ButtonPressedForegroundColor = Winui.Color.FromArgb(80, 255, 255, 255);
                titleBar.ButtonPressedBackgroundColor = Microsoft.UI.Colors.BlueViolet;

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Microsoft.UI.Colors.Gainsboro;
                titleBar.InactiveBackgroundColor = null;
                titleBar.ButtonInactiveForegroundColor = Microsoft.UI.Colors.AliceBlue;
                titleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
                break;
        }

        //titleBar.SetDragRectangles

        return true;
    }

    bool TrySetDragRectangles()
    {
        var rects = LoadRects();
        if (rects == null)
            return false;

        LoadTrigger();
        SetDragRectangles(rects);

        _IsLoaded = true;

        return true;
    }

    List<Rect>? LoadRects()
    {
        var bindableObjects = AppTitleBarExproperty.GetBindableObject();
        if (bindableObjects is null)
            return default;

        if (!bindableObjects.Any())
            return default;

        List<Rect> rects = new();
        foreach (var objItem in bindableObjects)
        {
            if (objItem is not View viewElement)
                continue;

            var bounds = viewElement.Bounds;
            if (double.IsNaN(bounds.Width) || double.IsNaN(bounds.Height))
                return default;

            if (double.IsInfinity(bounds.Width) || double.IsInfinity(bounds.Height))
                return default;

            if (double.IsNegative(bounds.Width) || double.IsNegative(bounds.Height))
                return default;

            if (double.IsNegativeInfinity(bounds.Width) || double.IsNegativeInfinity(bounds.Height))
                return default;

            var horizontalOptions = viewElement.HorizontalOptions;
            if (horizontalOptions.Alignment is LayoutAlignment.Center && bounds.X == 0)
            {
                var parent = viewElement.GetFirstParentWithAlignmentNotCenter();
                if (parent is not null)
                    bounds = parent.Bounds;
            }

            rects.Add(bounds);
        }

        return rects;
    }

    bool LoadTrigger()
    {
        AppTitleBarExproperty.BindiableObjectChangedEvent += BindiableObject_Changed;
        return true;
    }

    bool SetDragRectangles(List<Rect> rects)
    {
        if (!Microsoft.UI.Windowing.AppWindowTitleBar.IsCustomizationSupported())
            return false;

        if (_MainWindow is null)
            return false;

        if (_AppWindow is null)
            return false;

        if (_StartupOptions.TitleBarKind is not WindowTitleBarKind.ExtendsContentIntoTitleBar)
            return false;

        if (_StartupOptions.PresenterKind is WindowPresenterKind.FullScreen)
            return false;

        var titleBar = _AppWindow.TitleBar;
        if (titleBar is null)
            return false;

        double titleHeight = 48;
        var bounds = _MainWindow.Bounds;
#if DEBUG
        double debugWidth = 250;  
        double debugX = (bounds.Width - debugWidth) / 2.0d;
        Rect leftRect = new(debugX, 0, debugWidth, titleHeight);
        rects.Add((leftRect));
#endif
        double minX = rects[0].X;
        double maxX = rects[0].X;
        foreach (var rectItem in rects)
        {
            var x = rectItem.X;
            if (x < minX)
                minX = x;

            x = x + rectItem.Width;
            if (x > maxX)
                maxX = x;
        }

        List<RectInt32> rectInt32s = new();
        var scaleFactorPercent = _MainWindow.GetScaleAdjustment();

        int startX = 0;
        int startY = 0;
        int endX = (int)(minX * scaleFactorPercent);
        int endY = (int)(titleHeight * scaleFactorPercent);
        RectInt32 rectInt32 = new RectInt32(startX, startY, endX - startX, endY - startY);
        rectInt32s.Add(rectInt32);

        startX = (int)(maxX * scaleFactorPercent);
        endX = (int)(bounds.X + bounds.Width);
        rectInt32 = new RectInt32(startX, startY, endX - startX, endY - startY);
        rectInt32s.Add(rectInt32);

        if (_IsLoaded)
        {
            titleBar.ResetToDefault();
            RemoveTitleBar(_StartupOptions.TitleBarKind);
        }

        titleBar.SetDragRectangles(rectInt32s.ToArray());

        return true;
    }

    private void BindiableObject_Changed(object? sender , BindableObjectEvenArgs args)
    {
        var rects = LoadRects();
        if (rects == null)
            return;

        SetDragRectangles(rects);
    }

    bool MoveWindow(WindowPresenterKind presenter) => presenter switch
    {
        WindowPresenterKind.Default => MoveWindow(_StartupOptions.Location, _StartupOptions.Size),
        WindowPresenterKind.Maximize => MoveWindowMaximize(),
        WindowPresenterKind.Minimize => MoveWindowMinimize(),
        WindowPresenterKind.FullScreen => ToggleFullScreen(true),
        _ => false,
    };

    bool ToggleFullScreen(bool bFullScreen)
    {
        if (_AppWindow is null)
            return false;

        if (bFullScreen)
        {
            if (_AppWindow.Presenter.Kind is not Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen)
                _AppWindow.SetPresenter(Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen);
        }
        else
        {
            if (_AppWindow.Presenter.Kind is Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen)
                _AppWindow.SetPresenter(Microsoft.UI.Windowing.AppWindowPresenterKind.Default);
        }

        return true;
    }

    bool MoveWindow(WidnowAlignment location, Size size)
    {
        if (_MainWindow is null)
            return false;

        if (_AppWindow is null)
            return false;

        ToggleFullScreen(false);

        var width = size.Width;
        var height = size.Height;

        if (width < 0)
            width = 0;

        if (height < 0)
            height = 0;

        int screenWidth = User32.GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        int screenHeight = User32.GetSystemMetrics(SystemMetric.SM_CYSCREEN);

        double scalingFactor = _MainWindow.GetDisplayDensity();
        width = width * scalingFactor;
        height = height * scalingFactor;

        if (width > screenWidth)
            width = screenWidth;

        if (height > screenHeight)
            height = screenHeight;

        double startX = 0;
        double startY = 0;

        switch (location)
        {
            case WidnowAlignment.LeftTop:
                break;
            case WidnowAlignment.RightTop:
                startX = (screenWidth - width);
                break;
            case WidnowAlignment.Center:
                startX = (screenWidth - width) / 2.0;
                startY = (screenHeight - height) / 2.0;
                break;
            case WidnowAlignment.LeftBottom:
                startY = (screenHeight - height);
                break;
            case WidnowAlignment.RightBottom:
                startX = (screenWidth - width);
                startY = (screenHeight - height);
                break;
            default:
                break;
        }

        _AppWindow.MoveAndResize(new Windows_Graphics.RectInt32((int)startX, (int)startY, (int)width, (int)height));
        return true;
    }

    bool MoveWindowMaximize()
    {
        if (_MainWindow is null)
            return false;

        var windowHanlde = _MainWindow.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_MAXIMIZE), IntPtr.Zero);
        return true;
    }

    bool MoveWindowMinimize()
    {
        if (_MainWindow is null)
            return false;

        var windowHanlde = _MainWindow.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_MINIMIZE), IntPtr.Zero);
        return true;
    }

    bool MoveWindowRestore()
    {
        if (_MainWindow is null)
            return false;

        var windowHanlde = _MainWindow.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_RESTORE), IntPtr.Zero);
        return true;
    }

    bool IWindowsService.ResizeWindow(Size size) => MoveWindow(_StartupOptions.Location, size);

    bool IWindowsService.RestoreWindow() => MoveWindowRestore();

    bool IWindowsService.SetWindowMaximize() => MoveWindowMaximize();

    bool IWindowsService.SetWindowMinimize() => MoveWindowMinimize();

    bool IWindowsService.SwitchWindow(bool fullScreen) => ToggleFullScreen(fullScreen);
}
