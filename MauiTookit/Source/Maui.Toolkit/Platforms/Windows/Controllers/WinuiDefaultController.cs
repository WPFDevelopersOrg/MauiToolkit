using Maui.Toolkit.Core;
using Microsoftui = Microsoft.UI.Xaml;

namespace Maui.Toolkit.Platforms.Windows.Controllers;
internal class WinuiDefaultController : IBackdropController
{
    public WinuiDefaultController(Microsoftui.Window window)
    {

    }

    bool IBackdropController.Run()
    {
        return true;
    }

    bool IBackdropController.Stop()
    {
        return true;
    }
}
