namespace Maui.Toolkitx;
// All the code in this file is only included on Tizen.
internal class WindowService : IWindowService
{
    public WindowService(Window window, WindowChrome windowChrome)
    {
        _Window = window;
    }

    readonly Window _Window;

    bool IService.Run()
    {
        return true;
    }

    bool IService.Stop()
    {
        return true;
    }

    bool IWindowService.SetBackdropsKind(BackdropsKind kind)
    {
        return true;
    }
}
