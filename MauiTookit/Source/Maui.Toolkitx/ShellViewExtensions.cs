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

    public static Window UseShellView(this Window window, Action<ShellView>? configureDelegate)
    {
        var shellView = new ShellView();
        configureDelegate?.Invoke(shellView);
        ShellView.SetShellView(window, shellView);
        return window;
    }

    public static IShellViewService? GetWindowChromeService(this Window window)
    {
        if (window == null)
            return default;

        var worker = ShellViewWorker.GetShellViewWorker(window);
        if (worker is not IProvider<IShellViewService> provider)
            return default;

        return provider.GetService();
    }
}
