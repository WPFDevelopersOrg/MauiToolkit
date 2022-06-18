﻿using Maui.Toolkit.Core;
using Maui.Toolkit.Options;
using Maui.Toolkit.Providers;
using Maui.Toolkit.Services;

namespace Maui.Toolkit.Platforms;

internal class NavigationViewServiceImp : INavigationViewService, INatvigationViewProvider
{
    public NavigationViewServiceImp(ShellViewOptions options)
    {

    }

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddMac(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {


            }).OnResignActivation(app =>
            {

            }).ContinueUserActivity((app, user, handler) =>
            {

                return true;

            }).DidEnterBackground(app =>
            {

            }).WillFinishLaunching((app, options) =>
            {
                return true;
            }).FinishedLaunching((app, options) =>
            {
                return true;
            }).OpenUrl((app, url, options) =>
            {
                return true;
            }).PerformActionForShortcutItem((app, item, handler) =>
            {

            }).WillEnterForeground(app =>
            {

            }).WillTerminate(app =>
            {

            }).SceneWillConnect((scrne, session, options) =>
            {

            }).SceneDidDisconnect(scene =>
            {

            });
        });

        return true;
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
