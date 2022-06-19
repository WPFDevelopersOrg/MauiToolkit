using Maui.Toolkit.Core;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.Windows.Controllers;
using Maui.Toolkit.Providers;
using Maui.Toolkit.Services;
using Microsoft.Maui.Platform;
using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkit.Platforms;

internal class NavigationViewServiceImp : INavigationViewService, INatvigationViewProvider
{
    public NavigationViewServiceImp(ShellViewOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _Options = options;
    }

    readonly ShellViewOptions _Options;
    bool _IsRegister = false;

    WinuiWindowRootViewController? _RootNavigationViewBuilder;

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        ArgumentNullException.ThrowIfNull(lifecycleBuilder, nameof(lifecycleBuilder));

        lifecycleBuilder.AddWindows(windowsLeftCycle =>
        {
            windowsLeftCycle.OnWindowCreated(window =>
            {
                if (_IsRegister)
                    return;

                _IsRegister = true;

                _RootNavigationViewBuilder = CreateShellViewBuilder(window);
                IController? controller = _RootNavigationViewBuilder;
                controller?.Run();

            }).OnVisibilityChanged((window, arg) =>
            {

            }).OnActivated((window, arg) =>
            {


            }).OnLaunching((application, arg) =>
            {

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


    WinuiWindowRootViewController? CreateShellViewBuilder(MicrosoftuiXaml.Window window)
    {
        if (window is null)
            return default;

        if (window.Content is not WindowRootView windowRootView)
            return default;

        return new WinuiWindowRootViewController(windowRootView, _Options);
    }


    INavigationViewBuilder? INatvigationViewProvider.CreateNavigationViewBuilder(in VisualElement visualElement)
    {
        return default;
    }

    INavigationViewBuilder? INatvigationViewProvider.CreateShellViewBuilder(in Window window)
    {
        return default;
    }



    INavigationViewBuilder? INatvigationViewProvider.GetRootShellViewBuilder()
    {
        return default;
    }

    bool INavigationViewService.SetAppIcon(string icon)
    {
        return true;
    }

    bool INavigationViewService.SetBackButtonVisible(bool isVisible)
    {
        return true;
    }

    bool INavigationViewService.SetBackground(Brush brush)
    {
        return true;
    }

    bool INavigationViewService.SetBackgroundColor(Color color)
    {
        return true;
    }

    bool INavigationViewService.SetContentBackground(Brush brush)
    {
        return true;
    }

    bool INavigationViewService.SetContentBackgroundColor(Color color)
    {
        return true;
    }

    bool INavigationViewService.SetSearchBarVisible(bool isVisible)
    {
        return true;
    }

    bool INavigationViewService.SetSettingsVisible(bool isVisible)
    {
        return true;
    }

    bool INavigationViewService.SetTitle(string title)
    {
        return true;
    }

    bool INavigationViewService.SetTitleBarFontSize(double size)
    {
        return true;
    }

    bool INavigationViewService.SetTitleBarHeight(double height)
    {
        return true;
    }

    bool INavigationViewService.SetToggleButtonVisible(bool isVisible)
    {
        return true;
    }
}
