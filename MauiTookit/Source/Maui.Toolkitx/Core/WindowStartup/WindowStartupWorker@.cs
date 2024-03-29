﻿namespace Maui.Toolkitx.Core;

internal partial class WindowStartupWorker : IAttachedObject
{
    public WindowStartupWorker(WindowStartup windowStartup)
    {
        _WindowStartup = windowStartup;
    }

    readonly WindowStartup _WindowStartup;

    bool _IsAttached = false;

    IService? _Service;

    Window? _AssociatedObject;
    BindableObject? IAttachedObject.AssociatedObject => _AssociatedObject;
    bool IAttachedObject.IsAttached => _IsAttached;

    public void Attach(BindableObject bindableObject)
    {
        if (_IsAttached)
            return;

        if (bindableObject is not Window window)
            return;

        _AssociatedObject = window;

        if (window.Handler?.PlatformView is not null)
        {
            _Service = PlatformHelper.GetPlatformWindowStartupSevice(window, _WindowStartup);
            _Service?.Run();
        }

        window.HandlerChanging += Window_HandlerChanging;
        window.HandlerChanged += Window_HandlerChanged;
        window.Created += Window_Created;
        window.Deactivated += Window_Deactivated;
        window.Destroying += Window_Destroying;
        window.Stopped += Window_Stopped;
        _IsAttached = true;
    }

    private void Window_Deactivated(object? sender, EventArgs e)
    {
        
    }

    public void Detach()
    {
        if (!_IsAttached)
            return;

#if MACCATALYST
            _Service?.Stop();
#else
            if (_AssociatedObject is not null)
            {
                _AssociatedObject.HandlerChanging -= Window_HandlerChanging;
                _AssociatedObject.HandlerChanged -= Window_HandlerChanged;
                _AssociatedObject.Created -= Window_Created;
                _AssociatedObject.Destroying -= Window_Destroying;
                _AssociatedObject.Stopped -= Window_Stopped;
            }

            _IsAttached = false;
            _Service?.Stop();
            _Service = default;
            _AssociatedObject = default;
#endif
    }

    private void Window_Created(object? sender, EventArgs e)
    {

    }

    private void Window_HandlerChanging(object? sender, HandlerChangingEventArgs e)
    {
        if (_Service is not null)
            return;

        if (sender is not Window window)
            return;

        //_Service = PlatformHelper.GetPlatformWindowStartupSevice(window, _WindowStartup);
        //_Service = PlatformHelper.GetPlatformWindowStartupSevice(window, e.NewHandler, _WindowStartup);
        //_Service?.Run();
    }

    private void Window_HandlerChanged(object? sender, EventArgs e)
    {
        if (_Service is not null)
            return;

        if (sender is not Window window)
            return;

        _Service = PlatformHelper.GetPlatformWindowStartupSevice(window, _WindowStartup);
        _Service?.Run();
    }

    private void Window_Destroying(object? sender, EventArgs e)
    {
        Detach();
    }


    private void Window_Stopped(object? sender, EventArgs e)
    {

    }


}
