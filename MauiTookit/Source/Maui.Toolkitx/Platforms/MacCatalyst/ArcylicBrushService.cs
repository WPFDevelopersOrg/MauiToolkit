using CoreGraphics;
using Maui.Toolkitx.Compositions;
using UIKit;

namespace Maui.Toolkitx;

internal class ArcylicBrushService : IArcylicBrushService
{
    public ArcylicBrushService(VisualElement visualElement, MauiAcrylicBrush mauiAcrylicBrush)
    {
        ArgumentNullException.ThrowIfNull(visualElement);
        _VisualElement = visualElement;
        _AcrylicBrush = mauiAcrylicBrush;
    }

    readonly VisualElement _VisualElement;
    readonly MauiAcrylicBrush _AcrylicBrush;

    UIVisualEffectView? _UIVisualEffectView;

    bool IService.Run()
    {
        LoadEvent();
        SetBlurEffect(_VisualElement.Handler?.PlatformView as UIView);

        UIWindow.Notifications.ObserveDidBecomeVisible((sender, arg) =>
        {
            SetBlurEffect(_VisualElement.Handler?.PlatformView as UIView);
        });

        UIWindow.Notifications.ObserveDidBecomeHidden((sender, arg) =>
        {

        });


        return true;
    }

   
    bool IService.Stop()
    {
        UnloadEvent();
        return true;
    }

    bool LoadEvent()
    {
        _VisualElement.HandlerChanged += VisualElement_HandlerChanged;
        _AcrylicBrush.PropertyChanged += AcrylicBrush_PropertyChanged;
        return true;
    }

    bool SetBlurEffect(UIView? uiView)
    {
        if (uiView is null)
            return false;

        if (_UIVisualEffectView is null)
        {
            var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Light);
            var visualEffectView = new UIVisualEffectView(blurEffect)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            _UIVisualEffectView = visualEffectView;
            if (_AcrylicBrush.TintLuminosityOpacity != null)
                _UIVisualEffectView.Alpha = new NFloat(_AcrylicBrush.TintLuminosityOpacity.Value);

            uiView.BackgroundColor = UIColor.Clear;
            
        }

        if (_UIVisualEffectView is null)
            return false;

        _UIVisualEffectView.Frame = uiView.Bounds;
        foreach (var item in uiView.Subviews)
        {
            if (item.Equals(_UIVisualEffectView))
                return true;
        }

        uiView.AddSubview(_UIVisualEffectView);
        return true;
    }

    bool UnloadEvent()
    {
        _VisualElement.HandlerChanged -= VisualElement_HandlerChanged;
        _AcrylicBrush.PropertyChanged -= AcrylicBrush_PropertyChanged;

        return true;
    }

    private void AcrylicBrush_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        
    }

    private void VisualElement_HandlerChanged(object? sender, EventArgs e)
    {
        var uiView = _VisualElement.Handler?.PlatformView as UIView;
        if (uiView is null)
            return;

        _UIVisualEffectView?.Dispose();
        _UIVisualEffectView = default;

        SetBlurEffect(uiView);
    }

    private void VisualElement_SizeChanged(object? sender, EventArgs e)
    {
         
    }

    UIViewController? GetUIViewController(UIView? view)
    {
        if (view is default(UIView))
            return default;

        UIResponder uiResponder = view.NextResponder;
        for (; ; )
        {
            if (uiResponder is null)
                break;

            if (uiResponder is UIViewController viewController)
                return viewController;

            uiResponder = uiResponder.NextResponder;
        }

        return default;
    }

}
