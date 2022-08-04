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
        LoadSettings(_ShellView.IsSettingVisible, _ShellView.SettingConfigurations);
        LoadInternalElemenet();
    }

    private void RootNavigationView_SelectionChanged(MicrosoftuixamlControls.NavigationView sender, MicrosoftuixamlControls.NavigationViewSelectionChangedEventArgs args)
    {
        if (!args.IsSettingsSelected)
            return;

        if (_ShellView.SettingConfigurations is null)
            return;

        _ShellView.SettingConfigurations.Selected?.Execute(default);
    }


    bool LoadBackground(Color? color)
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.Background = color?.ToPlatform();

        return true;
    }

    bool LoadContentBackground(Color? color)
    {
        if (_RootNavigationView is null)
            return false;

        var navigationViewContent = _RootNavigationView.Content;
        if (navigationViewContent is MauiNavigationView mauiNavigationView)
            mauiNavigationView.Background = color?.ToPlatform();

        return true;
    }

    bool LoadInternalElemenet()
    {

        //var closeButtonPropety = typeof(MauiNavigationView).GetProperty("NavigationViewCloseButton", BindingFlags.Instance | BindingFlags.NonPublic);
        //if (closeButtonPropety?.GetValue(_RootNavigationView) is MicrosoftuixamlControls.Button closeButton)
        //{
        //    closeButton.Visibility = MicrosoftuiXaml.Visibility.Visible;
        //}

        var topNavAreaProperty = typeof(MauiNavigationView).GetProperty("TopNavArea", BindingFlags.Instance | BindingFlags.NonPublic);
        if (topNavAreaProperty?.GetValue(_RootNavigationView) is MicrosoftuixamlControls.StackPanel topNavArea)
        {
            //topNavArea.Background = Colors.Red.ToPlatform();
        }

        var buttonHolderGridProperty = typeof(MauiNavigationView).GetProperty("ButtonHolderGrid", BindingFlags.Instance | BindingFlags.NonPublic);
        if (buttonHolderGridProperty?.GetValue(_RootNavigationView) is MicrosoftuixamlControls.Grid buttonHolderGrid)
        {
            buttonHolderGrid.Background = Colors.Transparent.ToPlatform();
        }

        var contentGridProperty = typeof(MauiNavigationView).GetProperty("ContentGrid", BindingFlags.Instance | BindingFlags.NonPublic);
        if (contentGridProperty?.GetValue(_RootNavigationView) is MicrosoftuixamlControls.Grid contentGrid)
        {
            contentGrid.Background = Colors.Transparent.ToPlatform();
        }

        var paneContentGridProperty = typeof(MauiNavigationView).GetProperty("PaneContentGrid", BindingFlags.Instance | BindingFlags.NonPublic);
        if (paneContentGridProperty?.GetValue(_RootNavigationView) is MicrosoftuixamlControls.Grid paneContentGrid)
        {
            paneContentGrid.Background = Colors.Transparent.ToPlatform();

            paneContentGrid.RegisterPropertyChangedCallback(MicrosoftuixamlControls.Grid.VisibilityProperty, (sender, dp) =>
            {

            });
        }

        var paneCustomContentBorderProperty = typeof(MauiNavigationView).GetProperty("PaneCustomContentBorder", BindingFlags.Instance | BindingFlags.NonPublic);
        if (paneCustomContentBorderProperty?.GetValue(_RootNavigationView) is MicrosoftuixamlControls.ContentControl paneCustomContentBorder)
        {
            //paneCustomContentBorder.Background = Colors.Red.ToPlatform();
        }


        return true;
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
        _RootNavigationView.RegisterPropertyChangedCallback(MicrosoftuixamlControls.NavigationView.DisplayModeProperty, OnDisplayModePropertyCanllBack);
        _RootNavigationView.RegisterPropertyChangedCallback(MicrosoftuixamlControls.NavigationView.IsPaneOpenProperty, OnIsPaneOpenPropertyCallBack);
        _RootNavigationView.RegisterPropertyChangedCallback(MicrosoftuixamlControls.NavigationView.IsPaneToggleButtonVisibleProperty, OnIsPaneToggleButtonVisiblePropertyChangedCallBack);
        //_RootNavigationView.CompactPaneLength = 30d;
        //_RootNavigationView.IsPaneVisible = false;
        //_RootNavigationView.PaneDisplayMode = MicrosoftuiControls.NavigationViewPaneDisplayMode.Top;
        //_RootNavigationView.CompactModeThresholdWidth = 20;

        //var resource = _Application.Resources;
        //resource["NavigationBackButtonHeight"] = 40;
        //resource["NavigationBackButtonWidth"] = 40;

        return true;
    }

    private void SearchBar_QuerySubmitted(MicrosoftuixamlControls.AutoSuggestBox sender, MicrosoftuixamlControls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
         
    }

    private void OnIsPaneOpenPropertyCallBack(MicrosoftuiXaml.DependencyObject sender, MicrosoftuiXaml.DependencyProperty dp)
    {
        if (!sender.Equals(_RootNavigationView))
            return;

        if (_RootNavigationView.PaneDisplayMode != MicrosoftuixamlControls.NavigationViewPaneDisplayMode.Left)
            return;

        if (!_RootNavigationView.IsPaneOpen)
        {
            var paneContentGridProperty = typeof(MauiNavigationView).GetProperty("PaneContentGrid", BindingFlags.Instance | BindingFlags.NonPublic);
            if (paneContentGridProperty?.GetValue(_RootNavigationView) is MicrosoftuixamlControls.Grid paneContentGrid)
            {
                //paneContentGrid.Visibility = MicrosoftuiXaml.Visibility.Visible;

            }
        }
    }

    private void OnDisplayModePropertyCanllBack(MicrosoftuiXaml.DependencyObject sender, MicrosoftuiXaml.DependencyProperty dp)
    {
        if (!sender.Equals(_RootNavigationView))
            return;

        if (_RootNavigationView.PaneDisplayMode != MicrosoftuixamlControls.NavigationViewPaneDisplayMode.Left)
            return;

        if (_RootNavigationView.IsPaneToggleButtonVisible != _ShellView.IsPaneToggleButtonVisible && _ShellView.IsPaneToggleButtonVisible)
            _RootNavigationView.IsPaneToggleButtonVisible = _ShellView.IsPaneToggleButtonVisible;
    }

    private void OnIsPaneToggleButtonVisiblePropertyChangedCallBack(MicrosoftuiXaml.DependencyObject sender, MicrosoftuiXaml.DependencyProperty dp)
    {
        if (!sender.Equals(_RootNavigationView))
            return;

        if (_RootNavigationView.PaneDisplayMode != MicrosoftuixamlControls.NavigationViewPaneDisplayMode.Left)
            return;

        if (_RootNavigationView.IsPaneToggleButtonVisible != _ShellView.IsPaneToggleButtonVisible && _ShellView.IsPaneToggleButtonVisible)
            _RootNavigationView.IsPaneToggleButtonVisible = _ShellView.IsPaneToggleButtonVisible;
    }
}
