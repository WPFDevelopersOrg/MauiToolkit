namespace Maui.Toolkitx;

internal partial class WindowChromeWorker : IAttachedObject
{
    public WindowChromeWorker(WindowChrome windowChrome)
    {
        _WindowChrome = windowChrome;
    }

    readonly WindowChrome _WindowChrome;

    bool _IsAttached = false;
    IWindowChromeService? _Service;

    Window? _AssociatedObject;
    BindableObject? IAttachedObject.AssociatedObject => _AssociatedObject;

    public void Attach(BindableObject bindableObject)
    {
        if (_IsAttached)
            return;

        if (bindableObject is not Window window)
            return;

        _AssociatedObject = window;

        if (window.Handler?.PlatformView is not null)
        {
            _Service = PlatformHelper.GetPlatformWindowChromeSevice(window, _WindowChrome);
            _Service?.Run();
        }

        _WindowChrome.PropertyChanged += WindowChrome_PropertyChanged;
        window.HandlerChanged += Window_HandlerChanged;
        window.Created += Window_Created;
        window.Destroying += Window_Destroying;
        window.Stopped += Window_Stopped;
        _IsAttached = true;
    }

    public void Detach()
    {
        _WindowChrome.PropertyChanged -= WindowChrome_PropertyChanged;

        if (_AssociatedObject is Window window)
        {
            window.HandlerChanged -= Window_HandlerChanged;
            window.Created -= Window_Created;
            window.Destroying -= Window_Destroying;
            window.Stopped -= Window_Stopped;
        }

        _IsAttached = false;
        _Service?.Stop();
        _Service = default;
        _AssociatedObject = default;
    }

    private void WindowChrome_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(WindowChrome.CaptionHeight):
                break;
            case nameof(WindowChrome.WindowTitleBarKind):
                break;
            default:
                break;
        }
    }

    private void Window_Created(object? sender, EventArgs e)
    {
        
    }

    private void Window_HandlerChanged(object? sender, EventArgs e)
    {
        if (_Service is not null)
            return;

        if (sender is not Window window)
            return;

        _Service = PlatformHelper.GetPlatformWindowChromeSevice(window, _WindowChrome);
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
