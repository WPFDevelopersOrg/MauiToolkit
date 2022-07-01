using Maui.Toolkitx.Platforms.Windows.Controllers;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal class WindowService : IWindowService
{
    public WindowService(Window window, WindowChrome windowChrome)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(windowChrome);
        _Window = window;
        _WindowChrome = windowChrome;
        _WindowController = new(window, windowChrome);
        _TitleBarController = new WinuiTitleBarController(window, windowChrome);
    }

    readonly Window _Window;
    readonly WindowChrome _WindowChrome;
    readonly WinuiWindowController _WindowController;
    readonly WinuiTitleBarController _TitleBarController;
    IService? _BackdropService;

    bool IService.Run()
    {
        ((IService)_WindowController).Run();
        ((IService)_TitleBarController).Run();
        _BackdropService?.Stop();
        switch (_WindowChrome.BackdropsKind)
        {
            case BackdropsKind.Default:
                break;
            case BackdropsKind.Mica:
                _BackdropService = new WinuiMicaController(_Window);
                break;
            case BackdropsKind.Acrylic:
                _BackdropService = new WinuiAcrylicController(_Window);
                break;
            case BackdropsKind.BlurEffect:
                break;
            default:
                break;
        }
        _BackdropService?.Run();
        return true;
    }

    bool IService.Stop()
    {
        _BackdropService?.Stop();
        ((IService)_WindowController).Stop();
        ((IService)_TitleBarController).Stop();
        return true;
    }

    bool IWindowService.SetBackdropsKind(BackdropsKind kind)
    {
        return true;
    }
}
