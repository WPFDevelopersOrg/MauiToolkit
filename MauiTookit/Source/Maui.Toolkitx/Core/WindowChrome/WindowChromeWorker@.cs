using Maui.Toolkitx.Core;
using Maui.Toolkitx.Helpers;
using Maui.Toolkitx.Services;

namespace Maui.Toolkitx;

internal partial class WindowChromeWorker : IAttachedObject
{
    bool _IsAttached = false;
    IWindowService? _Service;

    Window? _AssociatedObject;
    BindableObject? IAttachedObject.AssociatedObject => _AssociatedObject;

    public void Attach(BindableObject bindableObject)
    {
        if (_IsAttached)
            return;

        if (bindableObject is not Window window)
            return;

        _AssociatedObject = window;
        window.HandlerChanged += Window_HandlerChanged;
        _IsAttached = true;
    }

    public void Detach()
    {
        if (_AssociatedObject is Window window)
            window.HandlerChanged -= Window_HandlerChanged;

        _IsAttached = false;
        _Service?.Stop();
        _Service = default;
        _AssociatedObject = default;
    }

    private void Window_HandlerChanged(object? sender, EventArgs e)
    {
        if (_Service is not null)
            return;

        if (sender is not Window window)
            return;

        _Service = PlatformHelper.GetPlatformWindowSevice(window);
        _Service?.Run();
    }
}
