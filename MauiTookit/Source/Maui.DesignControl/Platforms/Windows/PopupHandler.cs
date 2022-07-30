using Maui.DesignControl.Platforms.Windows;
using Maui.DesignControl.Shared;
using Microsoft.Maui.Handlers;

namespace Maui.DesignControl.Handlers;

// All the code in this file is only included on Windows.
public partial class PopupHandler : ViewHandler<IPopup, PlatformPopup>
{
    protected override PlatformPopup CreatePlatformView()
    {
        return new PlatformPopup();
    }

    public static void MapPopup(PopupHandler handler, IPopup view)
    {

    }
}
