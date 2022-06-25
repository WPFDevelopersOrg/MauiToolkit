using Maui.Toolkit.Core;
using Maui.Toolkit.Extensions;
using Maui.Toolkit.ExtraDependents;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.Windows.Extensions;
using Maui.Toolkit.Platforms.Windows.Runtimes.User32;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using Microsoft.Maui.Platform;
using PInvoke;
using WinRT;
using static PInvoke.User32;
using Microsoftui = Microsoft.UI;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using Windowsgraphics = Windows.Graphics;
using MicrosoftuixamlData =Microsoft.UI.Xaml.Data;
using Winui = Windows.UI;

namespace Maui.Toolkit.Platforms.Windows.Controllers;

internal partial class WinuiWindowController : IController, IWindowsService
{
    public WinuiWindowController(MicrosoftuiXaml.Application app, MicrosoftuiXaml.Window window, StartupOptions options, bool isMainWindow = false)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(MicrosoftuiXaml.Application));
        ArgumentNullException.ThrowIfNull(window, nameof(MicrosoftuiXaml.Window));
        ArgumentNullException.ThrowIfNull(options, nameof(StartupOptions));
        _Application = app;
        _Window = window;
        _Options = options;
        _OptionsChange = options with { };

        _AppWindow = _Window.GetAppWindow();
        _IsMainWindow = isMainWindow;

        if (window.Content is WindowRootView windowRootView)
            _WindowRootView = windowRootView;
    }

    readonly StartupOptions _Options;
    readonly StartupOptions _OptionsChange;
    readonly MicrosoftuiXaml.Application _Application;
    readonly MicrosoftuiXaml.Window _Window;
    readonly WindowRootView? _WindowRootView;
    readonly MicrosoftuiWindowing.AppWindow? _AppWindow;
    readonly bool _IsMainWindow = false;

    RootNavigationView? _RootNavigationView = default;
    MicrosoftuiXaml.FrameworkElement? _TitleBar;

    double _Offset = 0;
    IBackdropController? _WinuiController;
    bool _IsLoaded = false;

    bool _IsTitleBarIsSet = false;

    bool IController.Run()
    {
        if (_WindowRootView is null)
            return false;

        _RootNavigationView = _WindowRootView.NavigationViewControl;

        LoadBackgroundMaterial(_Options.BackdropsKind);

        if (_IsMainWindow)
        {
            LoadWindowRootViewEvent();
            LoadMainWindowEvent();
            ShownInSwitchers(_Options.IsShowInTaskbar);
            MoveWindow(_Options.PresenterKind);
            RegisterApplicationThemeChangedEvent();
        }
        else
        {
            MoveWindow(WindowAlignment.Center, new Size(900, 450));
            ShownInSwitchers(false);
        }

        return true;
    }

    bool IController.Stop()
    {
        _WinuiController?.Stop();

        if (_IsMainWindow)
        {
            UnLoadWindowRootViewEvent();
            RemoveMainWindowEvent();
            UnLoadTrigger();
            UnregisterApplicationThemeChangedEvent();
        }

        return true;
    }

    bool LoadBackgroundMaterial(BackdropsKind kind)
    {
        switch (kind)
        {
            case BackdropsKind.Mica:
                LoadMica();
                break;
            case BackdropsKind.Acrylic:
                LoadAcrylic();
                break;
            default:
                _WinuiController?.Stop();
                _WinuiController = default;
                break;
        }

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

    bool LoadMica()
    {
        if (_Window is null)
            return false;

        _WinuiController?.Stop();
        _WinuiController = new WinuiMicaController(_Window);
        return _WinuiController.Run();
    }

    bool LoadAcrylic()
    {
        if (_Window is null)
            return false;

        _WinuiController?.Stop();
        _WinuiController = new WinuiAcrylicController(_Window);
        return _WinuiController.Run();
    }

    bool RemoveTitleBar(WindowTitleBarKind titleBar)
    {
        if (_Application is null)
            return false;

        if (_Window is null)
            return false;

        if (_AppWindow is null)
            return false;

        if (_IsTitleBarIsSet)
            _AppWindow.TitleBar?.ResetToDefault();

        switch (titleBar)
        {
            case WindowTitleBarKind.PlatformDefault:
                _Window.ExtendsContentIntoTitleBar = false;
                if (_TitleBar is not null)
                    _TitleBar.Visibility = MicrosoftuiXaml.Visibility.Collapsed;
                break;
            case WindowTitleBarKind.CustomTitleBarAndExtension:

                _Window.ExtendsContentIntoTitleBar = false;
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
                _Window.ExtendsContentIntoTitleBar = true;
                if (_TitleBar is not null)
                    _TitleBar.Visibility = MicrosoftuiXaml.Visibility.Visible;
                break;
        }

        var res = _Application.Resources;
        if (_Options.TitleBarBackgroundColor != null)
            res["WindowCaptionBackground"] = _Options.TitleBarBackgroundColor.MauiColor2WinuiBrush();

        if (_Options.TitleBarBackgroundInactiveColor != null)
            res["WindowCaptionBackgroundDisabled"] = _Options.TitleBarBackgroundInactiveColor.MauiColor2WinuiBrush();

        if (_Options.TitleBarForegroundColor != null)
            res["WindowCaptionForeground"] = _Options.TitleBarForegroundColor.MauiColor2WinuiBrush();

        if (_Options.TitleBarForegroundInactiveColor != null)
            res["WindowCaptionForegroundDisabled"] = _Options.TitleBarForegroundInactiveColor.MauiColor2WinuiBrush();

        TriggertTitleBarRepaint();

        _IsTitleBarIsSet = true;

        return true;
    }

    bool TriggertTitleBarRepaint()
    {
        var windowHanlde = _Window.GetWindowHandle();
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

    bool UnLoadWindowRootViewEvent()
    {
        if (_WindowRootView is null)
            return false;

        var titlBarEventHandle = typeof(WindowRootView).GetEvent("OnAppTitleBarChanged", BindingFlags.Instance | BindingFlags.NonPublic);
        if (titlBarEventHandle is not null)
        {
            var removeMethod = titlBarEventHandle.RemoveMethod;
            removeMethod?.Invoke(_WindowRootView, new object[] { new EventHandler(OnAppTitleBarChanged) });
        }

        return true;
    }

    bool LoadMainWindowEvent()
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.Loaded += WindowRootView_Loaded;
        _RootNavigationView.Unloaded += WindowRootView_Unloaded;
        _RootNavigationView.SizeChanged += WindowRootView_SizeChanged;

        return true;
    }

    bool RemoveMainWindowEvent()
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.Loaded -= WindowRootView_Loaded;
        _RootNavigationView.Unloaded -= WindowRootView_Unloaded;
        _RootNavigationView.SizeChanged -= WindowRootView_SizeChanged;
        return true;
    }

    void WindowRootView_SizeChanged(object sender, MicrosoftuiXaml.SizeChangedEventArgs e)
    {
        if (!_IsLoaded)
            return;

        var rects = LoadRects();
        if (rects == null)
            return;

        SetDragRectangles(rects);
    }

    void WindowRootView_Loaded(object sender, MicrosoftuiXaml.RoutedEventArgs e)
    {
        var propertyInfo = typeof(WindowRootView).GetProperty("AppTitleBar", BindingFlags.Instance | BindingFlags.NonPublic);
        var titleBar = propertyInfo?.GetValue(_WindowRootView);
        if (titleBar is MicrosoftuiXaml.FrameworkElement frameworkElement)
            _TitleBar = frameworkElement;

        RemoveTitleBar(_Options.TitleBarKind);
        if (_Options.TitleBarKind is not WindowTitleBarKind.Default)
            SetWindowConfigrations(_Options.ConfigurationKind);

        if (Application.Current?.MainPage is Shell shell)
        {
            if (shell.FlyoutBehavior == FlyoutBehavior.Locked)
                _Offset = shell.FlyoutWidth;
        }

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
        //Winui.WindowManagement.AppWindowTitleBar
        //var coreApplication = _Application;
        //var coreApplication = _Application.As<ICoreApplication>();
        //var titleBar1 = CoreApplication.GetCurrentView()?.TitleBar;

        TrySetDragRectangles();
    }

    void WindowRootView_Unloaded(object sender, MicrosoftuiXaml.RoutedEventArgs e)
    {

    }

    bool LoadTrigger()
    {
        AppTitleBarExProperty.BindiableObjectChangedEvent += BindiableObject_Changed;
        return true;
    }

    bool UnLoadTrigger()
    {
        AppTitleBarExProperty.BindiableObjectChangedEvent -= BindiableObject_Changed;
        return true;
    }

    bool SetWindowConfigrations(WindowConfigurationKind kind)
    {
        if (_AppWindow is null)
            return false;

        switch (kind)
        {
            case WindowConfigurationKind.HideAllButton:
                var customOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.CreateForContextMenu();
                _AppWindow.SetPresenter(customOverlappedPresenter);
                break;
            case WindowConfigurationKind.ShowAllButton:
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
                        overlappedPresenter.IsMinimizable = !kind.HasFlag(WindowConfigurationKind.DisableMinizable);
                        overlappedPresenter.IsMaximizable = !kind.HasFlag(WindowConfigurationKind.DisableMaximizable);
                        overlappedPresenter.IsResizable = !kind.HasFlag(WindowConfigurationKind.DisableResizable);
                    }
                }
                break;
        }

        return true;
    }
    
    bool RestorConfigrations()
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

    bool MoveWindow(WindowPresenterKind presenter) => presenter switch
    {
        WindowPresenterKind.Default => MoveWindow(_Options.Location, _Options.Size),
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
            if (_OptionsChange.TitleBarKind is WindowTitleBarKind.Default)
                _Window.ExtendsContentIntoTitleBar = false;

            if (_AppWindow.Presenter.Kind is not MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
                _AppWindow.SetPresenter(MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen);
        }
        else
        {
            if (_OptionsChange.TitleBarKind is WindowTitleBarKind.Default)
                _Window.ExtendsContentIntoTitleBar = true;

            if (_AppWindow.Presenter.Kind is MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
            {
                var customOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.Create();
                _AppWindow.SetPresenter(customOverlappedPresenter);
            }
        }

        return true;
    }

    bool SetWindowPresenter(MicrosoftuiWindowing.AppWindowPresenterKind kind)
    {
        if (_AppWindow is null)
            return false;

        _AppWindow.SetPresenter(kind);

        return true;
    }

    bool SetWindowModal()
    {
        SetWindowPresenter(MicrosoftuiWindowing.AppWindowPresenterKind.Overlapped);

        if (_AppWindow is null)
            return false;

        //if (_AppWindow.Presenter.Kind is MicrosoftuiWindowing.AppWindowPresenterKind.Overlapped)
        //{
        //    var overlappedPresenter = _AppWindow.Presenter.As<MicrosoftuiWindowing.OverlappedPresenter>();
        //    if (overlappedPresenter is not null)
        //        overlappedPresenter.IsModal = true;
        //}

        var customOverlappedPresenter = MicrosoftuiWindowing.OverlappedPresenter.CreateForDialog();
        _AppWindow.SetPresenter(customOverlappedPresenter);

        return true;
    }

    bool MoveWindow(WindowAlignment location, Size size)
    {
        if (_Window is null)
            return false;

        if (_AppWindow is null)
            return false;

        var width = size.Width;
        var height = size.Height;

        if (width < 0)
            width = 0;

        if (height < 0)
            height = 0;

        int screenWidth = User32.GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        int screenHeight = User32.GetSystemMetrics(SystemMetric.SM_CYSCREEN);

        double scalingFactor = _Window.GetDisplayDensity();
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
            case WindowAlignment.LeftTop:
                break;
            case WindowAlignment.RightTop:
                startX = (screenWidth - width);
                break;
            case WindowAlignment.Center:
                startX = (screenWidth - width) / 2.0;
                startY = (screenHeight - height) / 2.0;
                break;
            case WindowAlignment.LeftBottom:
                startY = (screenHeight - height);
                break;
            case WindowAlignment.RightBottom:
                startX = (screenWidth - width);
                startY = (screenHeight - height);
                break;
            default:
                break;
        }

        ToggleFullScreen(false);
            
        _AppWindow.MoveAndResize(new Windowsgraphics.RectInt32((int)startX, (int)startY, (int)width, (int)height));
        return true;
    }

    bool MoveWindowMaximize()
    {
        if (_Window is null)
            return false;

        var windowHanlde = _Window.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_MAXIMIZE), IntPtr.Zero);
        return true;
    }

    bool MoveWindowMinimize()
    {
        if (_Window is null)
            return false;

        var windowHanlde = _Window.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_MINIMIZE), IntPtr.Zero);
        return true;
    }

    bool MoveWindowRestore()
    {
        if (_Window is null)
            return false;

        var windowHanlde = _Window.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_RESTORE), IntPtr.Zero);
        return true;
    }

    bool ShownInSwitchers(bool isShownInSwitchers)
    {
        if (_AppWindow is null)
            return false;

        _AppWindow.IsShownInSwitchers = isShownInSwitchers;
        return true;
    }

    void Application_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        LoadTitleBarCorlor(_AppWindow?.TitleBar);
    }

    void BindiableObject_Changed(object? sender, BindableObjectEvenArgs args)
    {
        var rects = LoadRects();
        if (rects == null)
            return;

        SetDragRectangles(rects);
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
        var bindableObjects = AppTitleBarExProperty.GetBindableObject();
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

    bool SetDragRectangles(List<Rect> rects)
    {
        if (!Microsoftui.Windowing.AppWindowTitleBar.IsCustomizationSupported())
            return false;

        if (_Window is null)
            return false;

        if (_AppWindow is null)
            return false;

        if (_Options.TitleBarKind is not WindowTitleBarKind.CustomTitleBarAndExtension)
            return false;

        if (_Options.PresenterKind is WindowPresenterKind.FullScreen)
            return false;

        var titleBar = _AppWindow.TitleBar;
        if (titleBar is null)
            return false;

        double titleHeight = 48;
        var bounds = _Window.Bounds;
#if DEBUG
        double debugWidth = 250;
        double debugX = (bounds.Width - debugWidth) / 2.0d;
        Rect leftRect = new(debugX, 0, debugWidth, titleHeight);
        rects.Add((leftRect));
#endif

        //sort
        rects.Sort((rc1, rc2) =>
        {
            if (rc2.X >= rc1.X)
                return 0;
            else
                return 1;
        });

        //combine
        List<Rect> newRects = new();
        var tempRects = rects.ToList();
        for (; ; )
        {
            if (tempRects.Count <= 0)
                break;

            foreach (var tempItem in tempRects)
            {
                Rect newRect = Rect.Zero;
                bool bFlag = true;
                foreach (var rectItem in rects)
                {
                    if (tempItem == rectItem)
                        continue;

                    //isInclude;
                    if (rectItem.Contains(tempItem))
                    {
                        double x = Math.Min(rectItem.Left, tempItem.Left);
                        double x2 = Math.Max(rectItem.Right, tempItem.Right);

                        newRect = new Rect(x, 0, x2 - x, titleHeight);
                        tempRects.Remove(tempItem);
                        tempRects.Remove(rectItem);
                        bFlag = false;
                        break;
                    }

                    if (tempItem.Contains(rectItem))
                    {
                        double x = Math.Min(rectItem.Left, tempItem.Left);
                        double x2 = Math.Max(rectItem.Right, tempItem.Right);

                        newRect = new Rect(x, 0, x2 - x, titleHeight);
                        tempRects.Remove(tempItem);
                        tempRects.Remove(rectItem);
                        bFlag = false;
                        break;
                    }

                    //isIntersect;
                    if (rectItem.IntersectsWith(tempItem))
                    {
                        double x = Math.Min(rectItem.Left, tempItem.Left);
                        double x2 = Math.Max(rectItem.Right, tempItem.Right);

                        newRect = new Rect(x, 0, x2 - x, titleHeight);
                        tempRects.Remove(tempItem);
                        tempRects.Remove(rectItem);
                        bFlag = false;
                        break;
                    }

                    if (tempItem.IntersectsWith(rectItem))
                    {
                        double x = Math.Min(rectItem.Left, tempItem.Left);
                        double x2 = Math.Max(rectItem.Right, tempItem.Right);

                        newRect = new Rect(x, 0, x2 - x, titleHeight);
                        tempRects.Remove(tempItem);
                        tempRects.Remove(rectItem);
                        bFlag = false;
                        break;
                    }
                }

                if (bFlag)
                {
                    tempRects.Remove(tempItem);
                    newRects.Add(tempItem);
                }
                else
                    newRects.Add(newRect);

                break;
            }

        }


        List<Windowsgraphics.RectInt32> rectInt32s = new();
        var scaleFactorPercent = _Window.GetScaleAdjustment();

        for (int i = 0; i < newRects.Count; i++)
        {
            var rectBefore = newRects[i];
            if (i - 1 >= 0)
                rectBefore = newRects[i - 1];
            else
                rectBefore = new Rect((int)-_Offset, 0, 0, titleHeight);

            var rect = newRects[i];

            int startX = 0;
            int endX = 0;

            startX = (int)(rectBefore.Right + _Offset);
            endX = (int)(rect.Left + _Offset);

            if (endX - startX <= 0)
                continue;

            var rectInt32 = new Windowsgraphics.RectInt32(startX, 0, endX - startX, (int)titleHeight);
            rectInt32s.Add(rectInt32);
        }

        if (newRects.Count > 0)
        {
            var rectBefore = newRects[newRects.Count - 1];
            var rect = new Rect(bounds.Right, 0, bounds.Right, titleHeight);

            int startX = 0;
            int endX = 0;

            startX = (int)(rectBefore.Right + _Offset);
            endX = (int)(rect.Left + _Offset);

            if (endX - startX > 0)
            {
                var rectInt32 = new Windowsgraphics.RectInt32(startX, 0, endX - startX, (int)titleHeight);
                rectInt32s.Add(rectInt32);
            }
        }

        //RemoveTitleBar(_Options.TitleBarKind);


        titleBar.SetDragRectangles(rectInt32s.ToArray());

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

    private MicrosoftuiXaml.Thickness _NavigationViewContentMargin;
    public MicrosoftuiXaml.Thickness NavigationViewContentMargin
    {
        get => _NavigationViewContentMargin;
        set
        {
            _NavigationViewContentMargin = value;
            if (_OptionsChange.TitleBarKind is WindowTitleBarKind.CustomTitleBarAndExtension)
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
}


internal partial class WinuiWindowController
{
    bool IWindowsService.SetBackdrop(BackdropsKind kind) => LoadBackgroundMaterial(kind);

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind) 
    {
        _OptionsChange.TitleBarKind = kind;
        RemoveTitleBar(kind);
        if (kind is not WindowTitleBarKind.Default)
            SetWindowConfigrations(_Options.ConfigurationKind);
        else
            RestorConfigrations();

        return true;
    }

    bool IWindowsService.SetWindowMaximize() => MoveWindowMaximize();

    bool IWindowsService.SetWindowMinimize() => MoveWindowMinimize();

    bool IWindowsService.RestoreWindow() => MoveWindowRestore();

    bool IWindowsService.ResizeWindow(Size size) => MoveWindow(WindowAlignment.Center, size);

    bool IWindowsService.SwitchWindow(bool fullScreen) => ToggleFullScreen(fullScreen);

    bool IWindowsService.ShowInTaskBar(bool isShow) => ShownInSwitchers(isShow);
}