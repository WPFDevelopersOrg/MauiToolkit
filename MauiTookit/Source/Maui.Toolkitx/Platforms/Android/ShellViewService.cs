namespace Maui.Toolkitx;

internal partial class ShellViewService : IShellViewService, IService
{

    public ShellViewService(Window window, ShellFrame shellView)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(shellView);

        _Window = window;
        _ShellView = shellView;
    }

    readonly Window _Window;
    readonly ShellFrame _ShellView;

    bool IService.Run()
    {
        throw new NotImplementedException();
    }

    bool IService.Stop()
    {
        throw new NotImplementedException();
    }
}
