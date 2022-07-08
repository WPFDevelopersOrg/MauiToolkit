using Maui.Toolkitx.Config;
using Microsoft.Maui.Platform;
using System.Drawing.Printing;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;

namespace Maui.Toolkitx;

internal partial class ShellViewService
{
    private void RootNavigationView_Loaded(object sender, MicrosoftuiXaml.RoutedEventArgs e)
    {
        
    }

    private void RootNavigationView_SelectionChanged(MicrosoftuixamlControls.NavigationView sender, MicrosoftuixamlControls.NavigationViewSelectionChangedEventArgs args)
    {
        if (!args.IsSettingsSelected)
            return;

        if (_ShellView.SettingConfigurations is null)
            return;

        _ShellView.SettingConfigurations.Selected?.Execute(default);
    }


    bool LoadBackButton(Visibility visibility)
    {
        if (_RootNavigationView is null)
            return false;

        switch (visibility)
        {
            case Visibility.Visible:
                _RootNavigationView.IsBackButtonVisible = MicrosoftuixamlControls.NavigationViewBackButtonVisible.Visible;
                break;
            case Visibility.Hidden:
                _RootNavigationView.IsBackButtonVisible = MicrosoftuixamlControls.NavigationViewBackButtonVisible.Auto;
                break;
            case Visibility.Collapsed:
                _RootNavigationView.IsBackButtonVisible = MicrosoftuixamlControls.NavigationViewBackButtonVisible.Collapsed;
                break;
            default:
                break;
        }

        return true;
    }

    bool LoadSettings(bool isVisible, SettingConfigurations? config)
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.IsSettingsVisible = isVisible;

        var settingsItem = _RootNavigationView.SettingsItem as MicrosoftuixamlControls.NavigationViewItem;
        if (settingsItem is null)
            return true;

        if (config?.Height != null)
            settingsItem.Height = config.Height.Value;

        if (config?.Width != null)
            settingsItem.Width = config.Width.Value;

        if (config?.Margin is not null)
            settingsItem.Margin = config.Margin.Value.ToPlatform();
        
        return true;
    }

    bool LoadSearchBar(bool isVisible, SearchBarConfigurations? config)
    {
        if (_RootNavigationView is null)
            return false;

        if (_SearchBar is null)
        {
            _SearchBar = new()
            {
                IsSuggestionListOpen = true,
                PlaceholderText = config?.PlaceholderText,
                QueryIcon = new MicrosoftuixamlControls.SymbolIcon(MicrosoftuixamlControls.Symbol.Find),
                LightDismissOverlayMode = MicrosoftuixamlControls.LightDismissOverlayMode.Auto,
            };

            _RootNavigationView.AutoSuggestBox = _SearchBar;
        }

        _SearchBar.Visibility = isVisible ? MicrosoftuiXaml.Visibility.Visible : MicrosoftuiXaml.Visibility.Collapsed;
        _SearchBar.QuerySubmitted += SearchBar_QuerySubmitted;
        return true;
    }

    bool LoadToggleButton(bool isVisible)
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.IsPaneToggleButtonVisible = isVisible;
        //_RootNavigationView.PaneDisplayMode = MicrosoftuiControls.NavigationViewPaneDisplayMode.Top;
        return true;
    }

    private void SearchBar_QuerySubmitted(MicrosoftuixamlControls.AutoSuggestBox sender, MicrosoftuixamlControls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
         
    }
}
