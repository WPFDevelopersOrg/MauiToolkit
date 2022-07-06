namespace Maui.Toolkitx;
internal partial class WindowStartupWorker : IAttachedObject
{
    public WindowStartupWorker(WindowStartup windowStartup)
    {
        _WindowStartup = windowStartup;
    }

    readonly WindowStartup _WindowStartup;

    bool _IsAttached = false;

    IWindowStartupService? _Service;

    Window? _AssociatedObject;
    BindableObject? IAttachedObject.AssociatedObject => _AssociatedObject;

    public void Attach(BindableObject bindableObject)
    {
        if (_IsAttached)
            return;

        if (bindableObject is not Window window)
            return;

        _AssociatedObject = window;

        if (window.Handler.PlatformView is not null)
        {
            _Service = PlatformHelper.GetPlatformWindowStartupSevice(window, _WindowStartup);
            _Service?.Run();
        }


    }

    public void Detach()
    {
        //throw new NotImplementedException();
    }
}
