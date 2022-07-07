namespace Maui.Toolkitx;

internal partial class ShellViewService : IShellViewService
{

    public ShellViewService(Window window, ShellView shellView)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(shellView);

        _Window = window;
        _ShellView = shellView;
    }

    readonly Window _Window;
    readonly ShellView _ShellView;

    bool IService.Run()
    {
        throw new NotImplementedException();
    }

    bool IService.Stop()
    {
        throw new NotImplementedException();
    }
}
