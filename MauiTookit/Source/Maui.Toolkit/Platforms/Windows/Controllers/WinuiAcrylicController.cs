using MicrosoftBackdrops = Microsoft.UI.Composition.SystemBackdrops;
using Microsoftui = Microsoft.UI.Xaml;
using MicrosoftuiComposition = Microsoft.UI.Composition;

namespace Maui.Toolkit.Platforms.Windows.Controllers;
internal class WinuiAcrylicController : IWinuiController
{
    public WinuiAcrylicController(Microsoftui.Window window)
    {
        _Window = window;
    }

    bool _IsStart = false;
    Microsoftui.Window? _Window;

    bool IWinuiController.Run()
    {
        if (_IsStart)
            return true;

        if (!MicrosoftBackdrops.DesktopAcrylicController.IsSupported())
            return false;

        if (_Window is not null)
            _Window.Activated += Window_Activated;


        _IsStart = true;
        return true;
    }

    bool IWinuiController.Stop()
    {
        if (_IsStart)
        {
            if (_Window is not null)
                _Window.Activated -= Window_Activated;
        }

        _Window = default;
        _IsStart = false;
        return true;
    }

    private void Window_Activated(object sender, Microsoftui.WindowActivatedEventArgs args)
    {

    }
}
