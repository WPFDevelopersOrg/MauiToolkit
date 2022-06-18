using Maui.Toolkit.Controls;

namespace Maui.Toolkit.Core;
public interface INavigationViewBuilder
{
    string? Title { get; set; }

    string? AppIcon { get; set; }

    Color? ViewBackgroundColor { get; set; }

    Brush? ViewBackground { get; set; }

    Color? ViewContentBackgroundColor { get; set; }

    Brush? ViewContentBackground { get; set; }

    bool IsToggleButtonVisible { get; set; }

    Button BackButton { get; set; }

    SettingsControl? Settings { get; set; }

    SearchBar? SearchBar { get; set; }

    double ViewTitleBarHeight { get;set; }

    double ViewTileBarFontSize { get; set; }

    object? SelectedItem { get; set; }

    event EventHandler SelectionChanged;

    event EventHandler ItemInvoked;
}
