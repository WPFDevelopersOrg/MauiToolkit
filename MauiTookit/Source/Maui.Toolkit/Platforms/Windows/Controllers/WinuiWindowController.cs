using Maui.Toolkit.Core;
using Maui.Toolkit.Extensions;
using Maui.Toolkit.ExtraDependents;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.Windows.Extensions;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using Microsoft.Maui.Platform;
using PInvoke;
using Windows.Graphics;
using Windows.Graphics.Display;
using static PInvoke.User32;
using Microsoftui = Microsoft.UI;
using MicrosoftuiControls = Microsoft.UI.Xaml.Controls;
using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using Windowsgraphics = Windows.Graphics;
using Winui = Windows.UI;
using MicrosoftuixamlDocument = Microsoft.UI.Xaml.Documents;
using MicrosoftuixamlmediaImaging = Microsoft.UI.Xaml.Media.Imaging;
using Maui.Toolkit.Builders;

namespace Maui.Toolkit.Platforms.Windows.Controllers;

internal partial class WinuiWindowController : IWindowController, IWindowsService
{
    public WinuiWindowController(MicrosoftuiXaml.Application app, MicrosoftuiXaml.Window window, StartupOptions options, bool isMainWindow = false)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(MicrosoftuiXaml.Application));
        ArgumentNullException.ThrowIfNull(window, nameof(MicrosoftuiXaml.Window));
        ArgumentNullException.ThrowIfNull(options, nameof(StartupOptions));
        _Application = app;
        _Window = window;
        _Options = options;
        _AppWindow = _Window.GetAppWindow();
        _IsMainWindow = isMainWindow;
    }

    readonly MicrosoftuiXaml.Application _Application;
    readonly MicrosoftuiXaml.Window _Window;
    readonly StartupOptions _Options;
    readonly MicrosoftuiWindowing.AppWindow? _AppWindow;
    readonly bool _IsMainWindow = false;

    double _Offset = 0;
    IBackdropController? _WinuiController;
    bool _IsLoaded = false;

    bool _IsTitleBarIsSetExtension = false;

    bool IWindowController.Run()
    {
        //_Application.OnThisPlatform()

        lock (this)
        {
            LoadBackgroundMaterial(_Options.BackdropsKind);

            if (_IsMainWindow)
            {
                RemoveTitleBar(_Options.TitleBarKind);
                MoveWindow(_Options.PresenterKind);
                LoadMainWindowEvent();
                RegisterApplicationThemeChangedEvent();
            }
            else
                MoveWindow(WindowAlignment.Center, new Size(900, 450));
        }

        return true;
    }

    bool IWindowController.Stop()
    {
        lock (this)
        {
            _WinuiController?.Stop();

            if (_IsMainWindow)
            {
                UnLoadTrigger();
                RemoveMainWindowEvent();
                UnregisterApplicationThemeChangedEvent();
            }
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
        if (_Window is null)
            return false;

        if (_AppWindow is null)
            return false;

        switch (titleBar)
        {
            case WindowTitleBarKind.PlatformDefault:
                _Window.ExtendsContentIntoTitleBar = false;
                break;
            case WindowTitleBarKind.ExtendsContentIntoTitleBar:
                if (!MicrosoftuiWindowing.AppWindowTitleBar.IsCustomizationSupported())
                    break;

                _Window.ExtendsContentIntoTitleBar = false;
                _AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                _AppWindow.TitleBar.IconShowOptions = MicrosoftuiWindowing.IconShowOptions.HideIconAndSystemMenu;
                LoadTitleBarCorlor(_AppWindow.TitleBar);

                Volatile.Write(ref _IsTitleBarIsSetExtension, true);
                break;
            default:
                if (_Application is null)
                    break;




                //var applicationView = ApplicationView.GetForCurrentView();
                //if (applicationView is not null)
                //{
                //    applicationView.TitleBar.BackgroundColor = Colors.Red.MauiColor2WinuiColor();
                //}

                //var view = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
                //view.TitleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
                // view.TitleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

                var isTitleBarIsSetExtension = Volatile.Read(ref _IsTitleBarIsSetExtension);
                if (isTitleBarIsSetExtension)
                {
                    _AppWindow.TitleBar.ExtendsContentIntoTitleBar = false;
                    _AppWindow.TitleBar.ResetToDefault();
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

                break;
        }

        return true;
    }

    bool LoadTitleBarCorlor(MicrosoftuiWindowing.AppWindowTitleBar? titleBar)
    {
        if (titleBar is null)
            return false;

        if (_Options.TitleBarKind is not WindowTitleBarKind.ExtendsContentIntoTitleBar)
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
                titleBar.BackgroundColor = null;
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

    bool LoadMainWindowEvent()
    {

        //DisplayInformation.GetForCurrentView().DpiChanged += WinuiWindowController_DpiChanged;

        var mainPage = Application.Current?.MainPage;
        if (mainPage is null)
            return false;

        mainPage.Loaded += MainPage_Loaded;

        if (mainPage is Shell shell)
        {
            if (shell.CurrentPage is not null)
                shell.CurrentPage.SizeChanged += MainPage_SizeChanged;
        }
        else
            mainPage.SizeChanged += MainPage_SizeChanged;

        return true;
    }

    private void WinuiWindowController_DpiChanged(DisplayInformation sender, object args)
    {

    }

    bool RemoveMainWindowEvent()
    {
        var mainPage = Application.Current?.MainPage;
        if (mainPage is null)
            return false;

        mainPage.Loaded -= MainPage_Loaded;
        if (mainPage is Shell shell)
        {
            if (shell.CurrentPage is not null)
                shell.CurrentPage.SizeChanged -= MainPage_SizeChanged;
        }
        else
            mainPage.SizeChanged -= MainPage_SizeChanged;

        return true;
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

    bool SetDragRectangles(List<Rect> rects)
    {
        if (!Microsoftui.Windowing.AppWindowTitleBar.IsCustomizationSupported())
            return false;

        if (_Window is null)
            return false;

        if (_AppWindow is null)
            return false;

        if (_Options.TitleBarKind is not WindowTitleBarKind.ExtendsContentIntoTitleBar)
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


        List<RectInt32> rectInt32s = new();
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

            var rectInt32 = new RectInt32(startX, 0, endX - startX, (int)titleHeight);
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
                var rectInt32 = new RectInt32(startX, 0, endX - startX, (int)titleHeight);
                rectInt32s.Add(rectInt32);
            }
        }

        if (_IsLoaded)
        {
            titleBar.ResetToDefault();
            RemoveTitleBar(_Options.TitleBarKind);
        }

        titleBar.SetDragRectangles(rectInt32s.ToArray());

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
            if (_AppWindow.Presenter.Kind is not MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
                _AppWindow.SetPresenter(MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen);
        }
        else
        {
            if (_AppWindow.Presenter.Kind is MicrosoftuiWindowing.AppWindowPresenterKind.FullScreen)
                _AppWindow.SetPresenter(MicrosoftuiWindowing.AppWindowPresenterKind.Default);
        }

        return true;
    }

    bool MoveWindow(WindowAlignment location, Size size)
    {
        if (_Window is null)
            return false;

        if (_AppWindow is null)
            return false;

        //ToggleFullScreen(false);

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

        //if (_AppWindow is null)
        //return false;

        //_AppWindow.Hide();

        var windowHanlde = _Window.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_MINIMIZE), IntPtr.Zero);
        return true;
    }

    bool MoveWindowRestore()
    {
        if (_Window is null)
            return false;

        //if (_Options.Size.IsZero)
        //    MoveWindow(WindowAlignment.Center, new Size(1920, 1080));
        //else
        //    MoveWindow(WindowAlignment.Center, _Options.Size);

        var windowHanlde = _Window.GetWindowHandle();
        User32.PostMessage(windowHanlde, WindowMessage.WM_SYSCOMMAND, new IntPtr((int)SysCommands.SC_RESTORE), IntPtr.Zero);
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
        if (sender is Shell shell)
        {
            if (shell.FlyoutBehavior == FlyoutBehavior.Locked)
                _Offset = shell.FlyoutWidth;
        }

        //var applicationView = ApplicationView.GetForCurrentView();
        //if (applicationView is not null)
        //{
        //    applicationView.TitleBar.BackgroundColor = Colors.Red.MauiColor2WinuiColor();
        //}

        var window = _Window;

        if (sender is VisualElement visualElement)
        {
            var mauiContext = visualElement.Handler?.MauiContext;
            var rootManager = mauiContext?.GetNavigationRootManager();

            var windowRootView = rootManager?.RootView as WindowRootView;
            var navigationViewControl = windowRootView?.NavigationViewControl;

            if (navigationViewControl is not null)
            {
                //                if (windowRootView is not null)
                //                {
                //                    var titleBarTemplate = (MicrosoftuiXaml.DataTemplate)MicrospftuixamlMarkup.XamlReader.Load(
                //                        @"<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
                //                                        xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
                //                            <Grid x:Name=""AppTitleBar""  
                //      Height=""78"">
                //    <Grid.ColumnDefinitions>
                //        <ColumnDefinition x:Name=""LeftPaddingColumn"" Width=""0""/>
                //        <ColumnDefinition x:Name=""IconColumn"" Width=""Auto""/>
                //        <ColumnDefinition x:Name=""TitleColumn"" Width=""Auto""/>
                //        <ColumnDefinition x:Name=""LeftDragColumn"" Width=""*""/>
                //        <ColumnDefinition x:Name=""SearchColumn"" Width=""Auto""/>
                //        <ColumnDefinition x:Name=""RightDragColumn"" Width=""*""/>
                //        <ColumnDefinition x:Name=""RightPaddingColumn"" Width=""0""/>
                //    </Grid.ColumnDefinitions>

                //    <TextBlock x:Name=""TitleTextBlock"" 
                //               Text=""App title"" 
                //               Style=""{StaticResource CaptionTextBlockStyle}""
                //               Grid.Column=""2""
                //               VerticalAlignment=""Center""
                //               Margin=""4,0,0,0""/>
                //    <AutoSuggestBox Grid.Column=""4"" QueryIcon=""Find""
                //                    PlaceholderText=""Search""
                //                    VerticalAlignment=""Center""
                //                    Width=""260"" Margin=""4,0""/>
                //</Grid>
                //                          </DataTemplate>");




                //                    windowRootView.AppTitleBarTemplate = titleBarTemplate;
                //                }

                

                var propertyInfo1 = typeof(WindowRootView).GetProperty("AppFontIcon", BindingFlags.Instance | BindingFlags.NonPublic);
                if (propertyInfo1?.GetValue(windowRootView) is MicrosoftuiControls.Image image)
                {
                    var path = PlatformShared.CreatePathBuilder()
                                             .AddArgument("Resources")
                                             .AddArgument("AppIcon")
                                             .AddArgument("application128.ico")
                                             .Build();
                    Uri imageUri = new(path, UriKind.RelativeOrAbsolute);
                    MicrosoftuixamlmediaImaging.BitmapImage imageBitmap = new(imageUri);
                    image.Source = imageBitmap;
                    image.Width = 25;
                    image.Height = 25;

                    image.Margin = new MicrosoftuiXaml.Thickness(-5, 5, 0, 0);
                    //image.Visibility = MicrosoftuiXaml.Visibility.Visible;
                }

                var propertyInfo = typeof(WindowRootView).GetProperty("AppTitleBar", BindingFlags.Instance | BindingFlags.NonPublic);
                if (propertyInfo?.GetValue(windowRootView) is MicrosoftuiXaml.FrameworkElement frameworkElement)
                {
                    frameworkElement.Height = 50;
                    var textBlock = frameworkElement.GetFirstDescendant<MicrosoftuiControls.TextBlock>();
                    if (textBlock is not null)
                    {
                        textBlock.FontSize = 16;
                        textBlock.VerticalAlignment = MicrosoftuiXaml.VerticalAlignment.Center;
                    }

                    if (frameworkElement is MicrosoftuiControls.Border border)
                        border.Padding = new MicrosoftuiXaml.Thickness(0, 0, 0, 15);
                    else if (frameworkElement is MicrosoftuiControls.Grid grid)
                        grid.Padding = new MicrosoftuiXaml.Thickness(0, 0, 0, 15);
                }


                navigationViewControl.NavigationViewBackButtonMargin = new MicrosoftuiXaml.Thickness(10);
                navigationViewControl.IsBackButtonVisible = MicrosoftuiControls.NavigationViewBackButtonVisible.Visible;
                navigationViewControl.IsBackEnabled = true;
                navigationViewControl.Background = Colors.Transparent.MauiColor2WinuiBrush();
                //navigationViewControl.BackgroundSizing = MicrosoftuiControls.BackgroundSizing.OuterBorderEdge;
                //navigationViewControl.ElementSoundMode = MicrosoftuiXaml.ElementSoundMode.FocusOnly;
                //navigationViewControl.AlwaysShowHeader = true;
                //navigationViewControl.Header = "MyHeader";
                navigationViewControl.ShoulderNavigationEnabled = MicrosoftuiControls.NavigationViewShoulderNavigationEnabled.Always;
                navigationViewControl.IsSettingsVisible = true;
                navigationViewControl.AutoSuggestBox = new MicrosoftuiControls.AutoSuggestBox()
                {
                    IsSuggestionListOpen = true,
                    PlaceholderText = "Search",
                    QueryIcon = new MicrosoftuiControls.SymbolIcon(MicrosoftuiControls.Symbol.Find),
                    LightDismissOverlayMode = MicrosoftuiControls.LightDismissOverlayMode.Auto
                };
                navigationViewControl.IsTitleBarAutoPaddingEnabled = false;
                navigationViewControl.IsPaneVisible = true;
                navigationViewControl.IsTitleBarAutoPaddingEnabled = false;
                navigationViewControl.PaneDisplayMode = MicrosoftuiControls.NavigationViewPaneDisplayMode.Left;
                navigationViewControl.PaneTitle = "Menus";
                navigationViewControl.IsTabStop = false;
                navigationViewControl.SelectionFollowsFocus = MicrosoftuiControls.NavigationViewSelectionFollowsFocus.Disabled;
                navigationViewControl.ExpandedModeThresholdWidth = 500;
                var settingsItem = navigationViewControl.SettingsItem as MicrosoftuiControls.NavigationViewItem;
                if (settingsItem is not null)
                {
                    settingsItem.Height = 35;
                    settingsItem.Margin = new MicrosoftuiXaml.Thickness(0, 5, 0, 5);
                    //settingsItem.HorizontalAlignment = MicrosoftuiXaml.HorizontalAlignment.Center;
                    //settingsItem.HorizontalContentAlignment = MicrosoftuiXaml.HorizontalAlignment.Center;
                }


                //navigationViewControl.DisplayMode = MicrosoftuiControls.NavigationViewDisplayMode.Expanded;
                //navigationViewControl.BorderThickness = new MicrosoftuiXaml.Thickness(10);
            }

        }

        var appTitleBar = new MicrosoftuiControls.Border
        {
            IsHitTestVisible = true,
            VerticalAlignment = MicrosoftuiXaml.VerticalAlignment.Top,
            Height = 70,
            Child = new MicrosoftuiControls.TextBlock
            {
                VerticalAlignment = MicrosoftuiXaml.VerticalAlignment.Top,
                Margin = new MicrosoftuiXaml.Thickness(0, 15, 0, 0),
                Text = "Test123",
            },
        };

        MicrosoftuiControls.Grid.SetColumn(appTitleBar, 1);
        MicrosoftuiControls.Canvas.SetZIndex(appTitleBar, 1);



        // _Window?.SetTitleBar(appTitleBar);

        //ShellView



        TrySetDragRectangles();
    }

    private void Application_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        LoadTitleBarCorlor(_AppWindow?.TitleBar);
    }

    private void BindiableObject_Changed(object? sender, BindableObjectEvenArgs args)
    {
        var rects = LoadRects();
        if (rects == null)
            return;

        SetDragRectangles(rects);
    }
}


internal partial class WinuiWindowController
{
    bool IWindowsService.SetBackdrop(BackdropsKind kind) => LoadBackgroundMaterial(kind);

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind) => RemoveTitleBar(kind);

    bool IWindowsService.SetWindowMaximize() => MoveWindowMaximize();

    bool IWindowsService.SetWindowMinimize() => MoveWindowMinimize();

    bool IWindowsService.RestoreWindow() => MoveWindowRestore();

    bool IWindowsService.ResizeWindow(Size size) => MoveWindow(WindowAlignment.Center, size);

    bool IWindowsService.SwitchWindow(bool fullScreen) => ToggleFullScreen(fullScreen);
}