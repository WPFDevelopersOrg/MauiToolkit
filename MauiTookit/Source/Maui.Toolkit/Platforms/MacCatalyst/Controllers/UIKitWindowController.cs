using Maui.Toolkit.Core;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Toolkit.Platforms.MacCatalyst.Controllers;
internal class UIKitWindowController : IWindowController, IWindowsService
{
    public UIKitWindowController()
    {

    }

    bool IWindowController.Run()
    {
        throw new NotImplementedException();
    }

    bool IWindowController.Stop()
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.ResizeWindow(Size size)
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.RestoreWindow()
    {
        throw new NotImplementedException();
    }



    bool IWindowsService.SetBackdrop(BackdropsKind kind)
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind)
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SetWindowMaximize()
    {
        throw new NotImplementedException();
    }

    bool IWindowsService.SetWindowMinimize()
    {
        throw new NotImplementedException();
    }



    bool IWindowsService.SwitchWindow(bool fullScreen)
    {
        throw new NotImplementedException();
    }
}
