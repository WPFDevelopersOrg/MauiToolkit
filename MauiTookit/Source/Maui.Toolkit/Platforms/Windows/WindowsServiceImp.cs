using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.Windows.Controllers;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using System.Collections.Concurrent;
using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkit.Platforms;

internal class WindowsServiceImp : IWindowsService
{
    public WindowsServiceImp(StartupOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StartupOptions = options;
    }

    readonly StartupOptions _StartupOptions;

    MicrosoftuiXaml.Application? _Application;
    MicrosoftuiXaml.Window? _MainWindow;

    volatile ConcurrentDictionary<MicrosoftuiXaml.Window, Core.IController> _mapWindows = new();

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        ArgumentNullException.ThrowIfNull(lifecycleBuilder, nameof(lifecycleBuilder));

        lifecycleBuilder.AddWindows(windowsLeftCycle =>
        {
            windowsLeftCycle.OnWindowCreated(window =>
            {
                bool isMainWindow = false;
                if (_MainWindow is null)
                {
                    _MainWindow = window;
                    isMainWindow = true;
                }

                if (_Application is null)
                    return;

                Core.IController controller = new WinuiWindowController(_Application, window, _StartupOptions, isMainWindow);
                controller.Run();
                _mapWindows.GetOrAdd(window, controller);

            }).OnVisibilityChanged((window, arg) =>
            {

            }).OnActivated((window, arg) =>
            {

            }).OnLaunching((application, arg) =>
            {
                _Application = application;
            }).OnLaunched((application, arg) =>
            {

            }).OnPlatformMessage((w, arg) =>
            {

            }).OnResumed(window =>
            {

            }).OnClosed((window, arg) =>
            {
                if (!_mapWindows.TryRemove(window, out var value))
                    return;

                value?.Stop();
            });
        });

        return true;
    }

    bool IWindowsService.SetBackdrop(BackdropsKind kind)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetBackdrop(kind);
    }

    bool IWindowsService.SetTitleBar(WindowTitleBarKind kind)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetTitleBar(kind);
    }

    bool IWindowsService.ResizeWindow(Size size)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.ResizeWindow(size);
    }

    bool IWindowsService.RestoreWindow()
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.RestoreWindow();
    }

    bool IWindowsService.SetWindowMaximize()
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetWindowMaximize();
    }

    bool IWindowsService.SetWindowMinimize()
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SetWindowMinimize();
    }

    bool IWindowsService.SwitchWindow(bool fullScreen)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.SwitchWindow(fullScreen);
    }

    bool IWindowsService.ShowInTaskBar(bool isShow)
    {
        if (_MainWindow is null)
            return false;

        if (!_mapWindows.TryGetValue(_MainWindow, out var value))
            return false;

        if (value is not IWindowsService service)
            return false;

        return service.ShowInTaskBar(isShow);
    }
}
