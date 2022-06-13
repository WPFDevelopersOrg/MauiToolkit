using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using WinuiBinding = Microsoft.UI.Xaml.Data.Binding;
using WinuiBindingExpression = Microsoft.UI.Xaml.Data.BindingExpression;
using WinuiBrush = Microsoft.UI.Xaml.Media.Brush;
using WinuiResourceDictionary = Microsoft.UI.Xaml.ResourceDictionary;
using WinuiApplication = Microsoft.UI.Xaml.Application;
using System.Threading.Tasks;
using WinuiPoint = Windows.Foundation.Point;
using Microsoft.Maui.Platform;
using Maui.Toolkit.Disposables;
using Maui.Toolkit.Extensions;

namespace Maui.Toolkit.Platforms.Windows.Extensions;
public static class XFrameworkElementExtensions
{
    static readonly Lazy<ConcurrentDictionary<Type, DependencyProperty>> ForegroundProperties =
        new Lazy<ConcurrentDictionary<Type, DependencyProperty>>(() => new ConcurrentDictionary<Type, DependencyProperty>());

    public static T? GetResource<T>(this FrameworkElement element, string key, T? def = default)
    {
        if (element.Resources.TryGetValue(key, out var resource))
            return (T?)resource;

        return def;
    }

    public static WinuiBrush GetForeground(this FrameworkElement element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        return (WinuiBrush)element.GetValue(GetForegroundProperty(element));
    }

    public static WinuiBinding? GetForegroundBinding(this FrameworkElement element)
    {
        WinuiBindingExpression expr = element.GetBindingExpression(GetForegroundProperty(element));

        if (expr == null)
            return null;

        return expr.ParentBinding;
    }

    public static object GetForegroundCache(this FrameworkElement element)
    {
        WinuiBinding? binding = GetForegroundBinding(element);

        if (binding != null)
            return binding;

        return GetForeground(element);
    }

    public static void RestoreForegroundCache(this FrameworkElement element, object cache)
    {
        var binding = cache as WinuiBinding;
        if (binding != null)
            SetForeground(element, binding);
        else
            SetForeground(element, (WinuiBrush)cache);
    }

    public static void SetForeground(this FrameworkElement element, WinuiBrush foregroundBrush)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        element.SetValue(GetForegroundProperty(element), foregroundBrush);
    }

    public static void SetForeground(this FrameworkElement element, WinuiBinding binding)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        element.SetBinding(GetForegroundProperty(element), binding);
    }

    public static void UpdateVerticalTextAlignment(this Control platformControl, ITextAlignment textAlignment)
    {
        platformControl.VerticalAlignment = textAlignment.VerticalTextAlignment.ToPlatformVerticalAlignment();
    }

    internal static IEnumerable<T?> GetDescendantsByName<T>(this DependencyObject parent, string elementName) where T : DependencyObject
    {
        var myChildrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < myChildrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T t && elementName.Equals(child.GetValue(FrameworkElement.NameProperty)))
            {
                yield return t;
            }
            else
            {
                foreach (var subChild in child.GetDescendantsByName<T>(elementName))
                    yield return subChild;
            }
        }
    }

    internal static T? GetDescendantByName<T>(this DependencyObject parent, string elementName) where T : DependencyObject
    {
        var myChildrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < myChildrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T t && elementName.Equals(child.GetValue(FrameworkElement.NameProperty)))
                return t;
            else if (child.GetDescendantByName<T>(elementName) is T tChild)
                return tChild;
        }
        return null;
    }

    internal static T? GetFirstDescendant<T>(this DependencyObject element) where T : FrameworkElement
    {
        var count = VisualTreeHelper.GetChildrenCount(element);
        for (var i = 0; i < count; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(element, i);

            if ((child as T ?? GetFirstDescendant<T>(child)) is T target)
                return target;
        }
        return null;
    }

    internal static WinuiResourceDictionary CloneResources(this FrameworkElement element)
    {
        var rd = new WinuiResourceDictionary();

        foreach (var r in element.Resources)
            rd.TryAdd(r.Key, r.Value);

        return rd;
    }

    internal static void TryUpdateResource(this FrameworkElement element, object newValue, params string[] keys)
    {
        var rd = element?.Resources;

        if (rd == null)
            return;

        foreach (var key in keys)
        {
            if (rd?.ContainsKey(key) ?? false)
                rd[key] = newValue;
        }
    }

    static DependencyProperty? GetForegroundProperty(FrameworkElement element)
    {
        if (element is Control)
            return Control.ForegroundProperty;
        if (element is TextBlock)
            return TextBlock.ForegroundProperty;

        Type type = element.GetType();

        if (!ForegroundProperties.Value.TryGetValue(type, out var foregroundProperty))
        {
            if (XReflectionExtensions.GetFields(type).FirstOrDefault(f => f.Name == "ForegroundProperty") is not FieldInfo field)
                throw new ArgumentException("type is not a Foregroundable type");

            if (field.GetValue(null) is DependencyProperty property)
                ForegroundProperties.Value.TryAdd(type, property);

            return null;
        }

        return foregroundProperty;
    }

    internal static IEnumerable<T?> GetChildren<T>(this DependencyObject parent) where T : DependencyObject
    {
        int myChildrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < myChildrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T t)
                yield return t;
            else
            {
                foreach (var subChild in child.GetChildren<T>())
                    yield return subChild;
            }
        }
    }

    internal static bool IsLoaded(this FrameworkElement frameworkElement) =>
        frameworkElement.IsLoaded;

    internal static IDisposable OnLoaded(this FrameworkElement frameworkElement, Action action)
    {
        if (frameworkElement.IsLoaded())
        {
            action();
            return new XActionDisposable(() => { });
        }

        RoutedEventHandler? routedEventHandler = null;
        XActionDisposable disposable = new XActionDisposable(() =>
        {
            if (routedEventHandler != null)
                frameworkElement.Loaded -= routedEventHandler;
        });

        routedEventHandler = (_, __) =>
        {
            disposable.Dispose();
            action();
        };

        frameworkElement.Loaded += routedEventHandler;
        return disposable;
    }

    internal static IDisposable OnUnloaded(this FrameworkElement frameworkElement, Action action)
    {
        if (!frameworkElement.IsLoaded())
        {
            action();
            return new XActionDisposable(() => { });
        }

        RoutedEventHandler? routedEventHandler = null;
        XActionDisposable disposable = new XActionDisposable(() =>
        {
            if (routedEventHandler != null)
                frameworkElement.Unloaded -= routedEventHandler;
        });

        routedEventHandler = (_, __) =>
        {
            disposable.Dispose();
            action();
        };

        frameworkElement.Unloaded += routedEventHandler;

        return disposable;
    }

    internal static void Arrange(this IView view, FrameworkElement frameworkElement)
    {
        var rect = new Rect(0, 0, frameworkElement.ActualWidth, frameworkElement.ActualHeight);

        if (!view.Frame.Equals(rect))
            view.Arrange(rect);
    }


    internal static void SetApplicationResource(this FrameworkElement frameworkElement, string propertyKey, object? value)
    {
        if (value is null)
        {
            if (WinuiApplication.Current.Resources.TryGetValue(propertyKey, out value))
                frameworkElement.Resources[propertyKey] = value;
            else
                frameworkElement.Resources.Remove(propertyKey);
        }
        else
            frameworkElement.Resources[propertyKey] = value;
    }

    internal static WinuiPoint? GetLocationOnScreen(this UIElement element)
    {
        var ttv = element.TransformToVisual(element.XamlRoot.Content);
        WinuiPoint screenCoords = ttv.TransformPoint(new WinuiPoint(0, 0));
        return new WinuiPoint(screenCoords.X, screenCoords.Y);
    }

    internal static WinuiPoint? GetLocationOnScreen(this IElement element)
    {
        if (element.Handler?.MauiContext == null)
            return null;

        var view = element.ToPlatform();
        return
            view.GetLocationRelativeTo(view.XamlRoot.Content);
    }

    internal static WinuiPoint? GetLocationRelativeTo(this UIElement element, UIElement relativeTo)
    {
        var ttv = element.TransformToVisual(relativeTo);
        WinuiPoint screenCoords = ttv.TransformPoint(new WinuiPoint(0, 0));
        return new WinuiPoint(screenCoords.X, screenCoords.Y);
    }

    internal static WinuiPoint? GetLocationRelativeTo(this IElement element, UIElement relativeTo)
    {
        if (element.Handler?.MauiContext == null)
            return null;

        return
            element
                .ToPlatform()
                .GetLocationRelativeTo(relativeTo);
    }

    internal static WinuiPoint? GetLocationRelativeTo(this IElement element, IElement relativeTo)
    {
        if (element.Handler?.MauiContext == null)
            return null;

        return
            element
                .ToPlatform()
                .GetLocationRelativeTo(relativeTo.ToPlatform());
    }
}