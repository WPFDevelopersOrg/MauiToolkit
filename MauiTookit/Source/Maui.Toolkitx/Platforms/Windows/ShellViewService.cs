using MicrosoftuiWindowing = Microsoft.UI.Windowing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using Microsoftui = Microsoft.UI;
using WindowsUI = Windows.UI;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using Windowsgraphics = Windows.Graphics;
using MicrosoftuixamlData = Microsoft.UI.Xaml.Data;
using Microsoft.Maui.Platform;

namespace Maui.Toolkitx;

internal partial class ShellViewService : IService
{
    public ShellViewService(Window window, ShellView shellView)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(shellView);
        ArgumentNullException.ThrowIfNull(window.Handler?.PlatformView);

        _Window = window;
        _ShellView = shellView;

        _WinuiWindow = window.Handler.PlatformView as MicrosoftuiXaml.Window;

        if (_WinuiWindow?.Content is WindowRootView windowRootView)
        {
            _WindowRootView = windowRootView;
            _RootNavigationView = windowRootView.NavigationViewControl;
        }
    }

    readonly Window _Window;
    readonly ShellView _ShellView;

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
