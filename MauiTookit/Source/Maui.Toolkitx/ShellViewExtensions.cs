using Maui.Toolkitx.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Toolkitx;
public static class ShellViewExtensions
{
    public static Window UseShellView(this Window window) => window.UseShellView(default);

    public static Window UseShellView(this Window window, Action<ShellFrame>? configureDelegate)
    {
        var shellView = new ShellFrame();
        configureDelegate?.Invoke(shellView);
        ShellFrame.SetShellFrame(window, shellView);
        return window;
    }

    public static IShellViewService? GetWindowChromeService(this Window window)
    {
        if (window == null)
            return default;

        var worker = ShellFrameWorker.GetShellFrameWorker(window);
        if (worker is not IProvider<IShellViewService> provider)
            return default;

        return provider.GetService();
    }
}
