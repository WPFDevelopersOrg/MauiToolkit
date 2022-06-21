using Maui.Toolkit.Controls;
using Maui.Toolkit.Core;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.Windows.Extensions;
using Maui.Toolkit.Shared;
using Microsoft.Maui.Platform;
using MicrosoftuiControls = Microsoft.UI.Xaml.Controls;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using MicrosoftuixamlmediaImaging = Microsoft.UI.Xaml.Media.Imaging;

namespace Maui.Toolkit.Platforms.Windows.Controllers;
internal class WinuiWindowRootViewController : INavigationViewBuilder, IController
{
    public WinuiWindowRootViewController(WindowRootView windowRootView, ShellViewOptions options)
    {
        _WindowRootView = windowRootView;
        _Options = options;
    }

    readonly WindowRootView _WindowRootView;
    readonly ShellViewOptions _Options;
    RootNavigationView? _RootNavigationView = default;

    MicrosoftuiControls.AutoSuggestBox? _SearchBar = default;

    private void NavigationViewControl_Loaded(object sender, MicrosoftuiXaml.RoutedEventArgs e)
    {
        LoadAppTitleBar(_Options.TitleBarHeight, _Options.TitleFontSize);
        LoadAppIcon(_Options.Icon);
        LoadBackButton(_Options.BackButtonVisible, _Options.TitleBarHeight);
        LoadSettings(_Options.IsSettingsVisible, _Options.SettingsHeight, _Options.SettingsMargin);
        LoadSearchBar(_Options.IsSerchBarVisible, _Options.SearchaBarPlaceholderText);
        LoadToggleButton(_Options.IsPaneToggleButtonVisible);
        LoadBackground(_Options.BackgroundColor);
        LoadContentBackground(_Options.ContentBackgroundColor);
    }

    bool IController.Run()
    {
        if (_WindowRootView is null)
            return false;

        _RootNavigationView = _WindowRootView.NavigationViewControl;
        if (_RootNavigationView is not null)
            _RootNavigationView.Loaded += NavigationViewControl_Loaded;

        return true;
    }

    bool IController.Stop()
    {
        if (_WindowRootView is null)
            return false;

        if (_RootNavigationView is not null)
            _RootNavigationView.Loaded -= NavigationViewControl_Loaded;

        _RootNavigationView = default;

        return true;
    }

    string? INavigationViewBuilder.Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    string? INavigationViewBuilder.AppIcon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Color? INavigationViewBuilder.ViewBackgroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Brush? INavigationViewBuilder.ViewBackground { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Color? INavigationViewBuilder.ViewContentBackgroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Brush? INavigationViewBuilder.ViewContentBackground { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    bool INavigationViewBuilder.IsToggleButtonVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Button INavigationViewBuilder.BackButton { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    SettingsControl? INavigationViewBuilder.Settings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    SearchBar? INavigationViewBuilder.SearchBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    double INavigationViewBuilder.ViewTitleBarHeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    double INavigationViewBuilder.ViewTileBarFontSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    object? INavigationViewBuilder.SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    event EventHandler INavigationViewBuilder.SelectionChanged
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    event EventHandler INavigationViewBuilder.ItemInvoked
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
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
            var textBlock = frameworkElement.GetFirstDescendant<MicrosoftuiControls.TextBlock>();
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
        if (propertyInfo1?.GetValue(_WindowRootView) is MicrosoftuiControls.Image image)
        {
            Uri imageUri = new(icon, UriKind.RelativeOrAbsolute);
            MicrosoftuixamlmediaImaging.BitmapImage imageBitmap = new(imageUri);
            image.Source = imageBitmap;
            image.Width = 25;
            image.Height = 25;

            //image.Margin = new MicrosoftuiXaml.Thickness(-5, 5, 0, 0);
            //image.Visibility = MicrosoftuiXaml.Visibility.Visible;
        }

        return true;
    }

    bool LoadBackButton(VisibilityKind kind, double titleBarheight)
    {
        if (_RootNavigationView is null)
            return false;

        switch (kind)
        {
            case VisibilityKind.Default:
                break;
            case VisibilityKind.Visible:
                _RootNavigationView.IsBackButtonVisible = MicrosoftuiControls.NavigationViewBackButtonVisible.Visible;
                break;
            case VisibilityKind.Hide:
                _RootNavigationView.IsBackButtonVisible = MicrosoftuiControls.NavigationViewBackButtonVisible.Auto;
                break;
            case VisibilityKind.Collapsed:
                _RootNavigationView.IsBackButtonVisible = MicrosoftuiControls.NavigationViewBackButtonVisible.Collapsed;
                break;
            default:
                break;
        }

        if (titleBarheight > 0)
        {
            var offset = titleBarheight - 40d;
            //_RootNavigationView.NavigationViewBackButtonMargin = new MicrosoftuiXaml.Thickness(50, offset, 0, 0);
        }

        return true;
    }

    bool LoadSettings(bool isVisible, double? height, Thickness? margin)
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.IsSettingsVisible = isVisible;
        var settingsItem = _RootNavigationView.SettingsItem as MicrosoftuiControls.NavigationViewItem;
        if (settingsItem is null)
            return true;

        if (height is not null)
            settingsItem.Height = height.Value;

        if (margin is not null)
            settingsItem.Margin = margin.Value.MauiThickness2WinuiThickness();

        return true;
    }

    bool LoadSearchBar(bool isVisible, string? placeholderText)
    {
        if (_RootNavigationView is null)
            return false;

        if (_SearchBar is null)
        {
            _SearchBar = new()
            {
                IsSuggestionListOpen = true,
                PlaceholderText = placeholderText,
                QueryIcon = new MicrosoftuiControls.SymbolIcon(MicrosoftuiControls.Symbol.Find),
                LightDismissOverlayMode = MicrosoftuiControls.LightDismissOverlayMode.Auto,
            };

            _RootNavigationView.AutoSuggestBox = _SearchBar;
        }

       _SearchBar.Visibility = isVisible ? MicrosoftuiXaml.Visibility.Visible : MicrosoftuiXaml.Visibility.Collapsed;

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

    bool LoadBackground(Color? color)
    {
        if (_RootNavigationView is null)
            return false;

        _RootNavigationView.Background = color?.MauiColor2WinuiBrush();

        return true;
    }

    bool LoadContentBackground(Color? color)
    {
        if (_RootNavigationView is null)
            return false;

        var navigationViewContent = _RootNavigationView.Content;
        if (navigationViewContent is MauiNavigationView mauiNavigationView)
            mauiNavigationView.Background = color?.MauiColor2WinuiBrush();

        return true;
    }
}
