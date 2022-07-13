using Microsoft.Maui;
using Microsoft.Maui.Platform;
using System.Reflection;
using MicosoftuiXaml = Microsoft.UI.Xaml;
using MicosoftuixamlControls = Microsoft.UI.Xaml.Controls;

namespace Maui.Toolkitx.Platforms.Windows.Extensions;
public static class MauiNavigationViewExtensions
{

    public static NavigationRootManager GetNavigationRootManager(this IMauiContext mauiContext) => mauiContext.Services.GetRequiredService<NavigationRootManager>();

    public static bool IsBackButtonVisible(IElementHandler handler)
    {
        var mauiContext = handler.MauiContext;
        if (mauiContext is null)
            return false;

        return IsBackButtonVisible(mauiContext);
    }

    public static bool IsBackButtonVisible(IMauiContext mauiContext)
    {
        var navView = GetMauiNavigationView(mauiContext);
        return navView?.IsBackButtonVisible == MicosoftuixamlControls.NavigationViewBackButtonVisible.Visible;
    }

    public static bool IsNavigationBarVisible(IElementHandler handler)
    {
        var mauiContext = handler.MauiContext;
        if (mauiContext is null)
            return false;

        return IsNavigationBarVisible(mauiContext);
    }

    public static bool IsNavigationBarVisible(IMauiContext mauiContext)
    {
        var navView = GetMauiNavigationView(mauiContext);
        var header = navView?.Header as MicosoftuiXaml.FrameworkElement;
        return header?.Visibility == MicosoftuiXaml.Visibility.Visible;
    }

    public static MauiNavigationView? GetMauiNavigationView(NavigationRootManager navigationRootManager)
    {
        return (navigationRootManager.RootView as WindowRootView)?.NavigationViewControl;
    }

    public static MauiNavigationView? GetMauiNavigationView(IMauiContext mauiContext)
    {
        return GetMauiNavigationView(mauiContext.GetNavigationRootManager());
    }

    public static MauiToolbar? GetPlatformToolbar(IElementHandler handler)
    {
        var mauiContext = handler.MauiContext;
        if (mauiContext is null)
            return default;

        var navView = GetMauiNavigationView(mauiContext);
        MauiToolbar? windowHeader = navView?.Header as MauiToolbar;
        return windowHeader;
    }


    public static IEnumerable<MicosoftuixamlControls.NavigationViewItem> GetNavigationViewItems(MauiNavigationView navigationView)
    {
        if (navigationView.MenuItems?.Count > 0)
        {
            foreach (var menuItem in navigationView.MenuItems)
            {
                if (menuItem is MicosoftuixamlControls.NavigationViewItem item)
                    yield return item;
            }
        }
        else if (navigationView.MenuItemsSource != null)
        {
            var propertyInfo =  typeof(MauiNavigationView).GetProperty("TopNavMenuItemsHost", BindingFlags.Instance | BindingFlags.NonPublic);
            var itemsRepeater = propertyInfo?.GetValue(navigationView) as MicosoftuixamlControls.ItemsRepeater;
            if (itemsRepeater != null)
            {
                var itemCount = itemsRepeater.ItemsSourceView.Count;
                for (int i = 0; i < itemCount; i++)
                {
                    MicosoftuiXaml.UIElement uIElement = itemsRepeater.TryGetElement(i);

                    if (uIElement is MicosoftuixamlControls.NavigationViewItem item)
                        yield return item;
                }
            }
        }
    }

    public static double DistanceYFromTheBottomOfTheAppTitleBar(IElement element)
    {
        var handler = element.Handler;
        var rootManager = handler?.MauiContext?.GetNavigationRootManager();
        if (rootManager is null)
            return 0;

        var propertyInfo = typeof(NavigationRootManager).GetProperty("AppTitleBar", BindingFlags.Instance | BindingFlags.NonPublic);
        var titleBar = propertyInfo?.GetValue(rootManager) as MicosoftuiXaml.FrameworkElement;
        if (titleBar is null)
            return 0;

        var position = element.GetLocationRelativeTo(titleBar);
        if (position is null)
            return 0;

        var distance = titleBar.ActualHeight - position.Value.Y;
        return distance;
    }

    public static object? GetTitleView(IElementHandler handler)
    {
        var toolbar = GetPlatformToolbar(handler);

        var propertyInfo = typeof(MauiToolbar).GetProperty("TitleView", BindingFlags.Instance | BindingFlags.NonPublic);
        var titleView = propertyInfo?.GetValue(toolbar);
        return titleView;
    }
}
