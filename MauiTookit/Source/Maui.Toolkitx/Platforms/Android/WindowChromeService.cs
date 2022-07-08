namespace Maui.Toolkitx;

// All the code in this file is only included on Android.
internal class WindowChromeService : IWindowChromeService, IService
{
    public WindowChromeService(Window window, WindowChrome windowChrome)
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

     
}
