namespace Maui.Toolkit.Services;

public interface INavigationViewService
{
    bool SetAppIcon(string icon);

    bool SetTitle(string title);

    bool SetTitleBarFontSize(double size);

    bool SetTitleBarHeight(double height);

    bool SetBackButtonVisible(bool isVisible);

    bool SetToggleButtonVisible(bool isVisible);

    bool SetSearchBarVisible(bool isVisible);

    bool SetSettingsVisible(bool isVisible);

    bool SetBackgroundColor(Color color);

    bool SetBackground(Brush brush);

    bool SetContentBackgroundColor(Color color);

    bool SetContentBackground(Brush brush);

}
