using Maui.Toolkitx.Config;
using Maui.Toolkitx.Platforms.Windows.Runtimes.Shell32;
using PInvoke;
using static PInvoke.User32;

namespace Maui.Toolkitx;

internal partial class StatusBarService : IService
{
    public unsafe StatusBarService(StatusBarConfigurations config)
    {
        ArgumentNullException.ThrowIfNull(config);
        _Config = config;
        _WndProc = WndProc_Callback;
    }

    readonly StatusBarConfigurations _Config;
    readonly int _WmStatusBarMouseMessage = (int)WindowMessage.WM_USER + 1024;
    readonly WndProc _WndProc;
    IntPtr _StatusBarWindowHandle;
    int _WmStatusBarCreated;
    string? _StatusBarWindowClassName;

    NOTIFYICONDATA _NOTIFYICONDATA = default;
    bool _IsShowIn = false;
    IntPtr _hICon;
    IDisposable? _Disposable;

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        ArgumentNullException.ThrowIfNull(lifecycleBuilder, nameof(lifecycleBuilder));

        lifecycleBuilder.AddWindows(windowsLeftCycle =>
        {
            windowsLeftCycle.OnWindowCreated(window =>
            {
            }).OnVisibilityChanged((window, arg) =>
            {
            }).OnActivated((window, arg) =>
            {
            }).OnLaunching((application, arg) =>
            {
                ((IService)this).Run();
            }).OnLaunched((application, arg) =>
            {
            }).OnPlatformMessage((w, arg) =>
            {
            }).OnResumed(window =>
            {
            }).OnClosed((window, arg) =>
            {
                //关闭托盘服务
                ((IService)this).Stop();
            });
        });
        return true;
    }

    bool IService.Run()
    {
        //启动托盘服务
        RegisterClass();
        LoadNotifyIconData(_Config.Icon1, _Config.Title);
        Show();

        return true;
    }

    bool IService.Stop()
    {
        Hide();
        return true;
    }
}
