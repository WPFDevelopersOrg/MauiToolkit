﻿using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using Microsoftui = Microsoft.UI;
using WindowsUI = Windows.UI;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using Windowsgraphics = Windows.Graphics;
using MicrosoftuixamlData = Microsoft.UI.Xaml.Data;
using Microsoft.Maui.Platform;
using Maui.Toolkitx.Extensions;
using Microsoft.Maui.Controls.Platform;
using Maui.Toolkitx.Platforms.Windows.Extensions;

namespace Maui.Toolkitx;

internal partial class ShellViewService : IService
{
    public ShellViewService(Window window, ShellFrame shellView)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(shellView);
        ArgumentNullException.ThrowIfNull(window.Handler?.PlatformView);

        _Application = MicrosoftuiXaml.Application.Current;
        _Window = window;
        _ShellView = shellView;

        _WinuiWindow = window.Handler.PlatformView as MicrosoftuiXaml.Window;

        var mauiContext = _Window.Page?.RequireMauiContext();
        if (mauiContext is not null)
        {
            var platformElement = window.Page?.ToPlatform() as ShellView;
            _RootNavigationView = platformElement;

            _WindowRootView = mauiContext.GetNavigationRootManager().RootView as WindowRootView;
        }

        //if (_WinuiWindow?.Content is WindowRootView windowRootView)
        //{
        //    _WindowRootView = windowRootView;
        //    _RootNavigationView = windowRootView.NavigationViewControl;
        //}
    }

    readonly MicrosoftuiXaml.Application _Application;
    readonly Window _Window;
    readonly ShellFrame _ShellView;

    readonly MicrosoftuiXaml.Window? _WinuiWindow = default;
    readonly WindowRootView? _WindowRootView = default;
    readonly RootNavigationView? _RootNavigationView = default;

    MicrosoftuixamlControls.AutoSuggestBox? _SearchBar = default;

    bool IService.Run()
    {
        if (_RootNavigationView is not null)
        {
            _RootNavigationView.Loaded += RootNavigationView_Loaded;
            _RootNavigationView.SelectionChanged += RootNavigationView_SelectionChanged; 
        }

        LoadBackground(_ShellView.Background);
        LoadContentBackground(_ShellView.ContentBackground);
        LoadBackButton(_ShellView.BackButtonVisible);
        //LoadSettings(_ShellView.IsSettingVisible, _ShellView.SettingConfigurations);
        LoadSearchBar(_ShellView.IsSearchBarVisible, _ShellView.SearchBarConfigurations);
        LoadToggleButton(_ShellView.IsPaneToggleButtonVisible);

        //LoadInternalElemenet();

        return true;
    }

    bool IService.Stop()
    {
        if (_RootNavigationView is not null)
        {
            _RootNavigationView.Loaded -= RootNavigationView_Loaded;
            _RootNavigationView.SelectionChanged -= RootNavigationView_SelectionChanged;
        }

        if (_SearchBar is not null)
            _SearchBar.QuerySubmitted -= SearchBar_QuerySubmitted;

        return true;
    }
}
