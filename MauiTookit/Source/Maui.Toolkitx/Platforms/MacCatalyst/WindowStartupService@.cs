using Microsoft.Maui.Platform;
using UIKit;

namespace Maui.Toolkitx;

// All the code in this file is only included on Windows.
internal partial class WindowStartupService : IWindowStartupService
{


    bool IWindowStartupService.SetBackdropsKind(BackdropsKind kind)
    {
        return true;
    }
}
