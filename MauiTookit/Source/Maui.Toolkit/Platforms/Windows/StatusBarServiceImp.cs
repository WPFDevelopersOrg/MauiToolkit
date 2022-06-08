using Maui.Toolkit.Concurrency;
using Maui.Toolkit.Disposables;
using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.Windows.Runtimes;
using Maui.Toolkit.Platforms.Windows.Runtimes.Shell32;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using PInvoke;


namespace Maui.Toolkit.Platforms;

internal class StatusBarServiceImp : IStatusBarService
{
    public StatusBarServiceImp(StatusBarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StatusBarOptions = options;
    }

    readonly StatusBarOptions _StatusBarOptions;
    Microsoft.UI.Xaml.Window? _MainWindow;
    NOTIFYICONDATA _NOTIFYICONDATA = default;
    bool _IsShowIn = false;

    IntPtr _hICon;

    IDisposable? _Disposable;

    private event EventHandler<EventArgs>? StatusBarEventChanged;

    event EventHandler<EventArgs> IStatusBarService.StatusBarEventChanged
    {
        add => StatusBarEventChanged += value;
        remove => StatusBarEventChanged -= value;
    }

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        ArgumentNullException.ThrowIfNull(lifecycleBuilder, nameof(lifecycleBuilder));

        lifecycleBuilder.AddWindows(windowsLeftCycle =>
        {
            windowsLeftCycle.OnWindowCreated(window =>
            {
                _MainWindow = window;

                _NOTIFYICONDATA = NOTIFYICONDATA.GetDefaultNotifyData(_MainWindow.GetWindowHandle());

                ((IStatusBarService)this).Show(_StatusBarOptions.IconFilePath);

            }).OnVisibilityChanged((window, arg) =>
            {

            }).OnActivated((window, arg) =>
            {


            }).OnLaunching((application, arg) =>
            {

            }).OnLaunched((application, arg) =>
            {

            }).OnPlatformMessage((w, arg) =>
            {
                if (arg is null)
                    return;

                if (arg.MessageId == (uint)NOTIFYMESSAGESINK.NotifyCallBackMessage)
                {

                }

            }).OnResumed(window =>
            {

            }).OnClosed((window, arg) =>
            {
                ((IStatusBarService)this).Hide();
            });
        });
        return true;
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        lock (this)
        {
            if (!string.IsNullOrWhiteSpace(iconPath))
            {
                if (_hICon != IntPtr.Zero)
                    RuntimeInterop.DeleteObject(_hICon);

                IntPtr hIcon = User32.LoadImage(IntPtr.Zero, iconPath, User32.ImageType.IMAGE_ICON, 32, 32, User32.LoadImageFlags.LR_LOADFROMFILE);
                _NOTIFYICONDATA.hIcon = hIcon;
                _NOTIFYICONDATA.hBalloonIcon = hIcon;
                _hICon = hIcon;
            }

            //_NOTIFYICONDATA.TimeoutOrVersion = (uint)NOTIFYICONVERSIONFlags.NOTIFYICON_VERSION_4;
            //RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_SetVersion, ref _NOTIFYICONDATA);
            //
            if (Volatile.Read(ref _IsShowIn))
                return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Modify, ref _NOTIFYICONDATA);
            else
            {
                Volatile.Write(ref _IsShowIn, true);
                return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Add, ref _NOTIFYICONDATA);
            }
        }
    }

    bool IStatusBarService.Hide()
    {
        lock (this)
        {
            Volatile.Write(ref _IsShowIn, false);
            _Disposable?.Dispose();
            return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Delete, ref _NOTIFYICONDATA);
        }
    }

    bool IStatusBarService.SetDescription(string? text) => true;

    IDisposable IStatusBarService.SchedulePeriodic(TimeSpan period, Func<bool, string>? action)
    {
        if (_Disposable is not null)
            return _Disposable;

        var rate = period.TotalMilliseconds;
        if (rate <= 0)
            rate = 500;
        else if (rate > 1000)
            rate = 1000;

        period = TimeSpan.FromMilliseconds(rate);
        var scheduler = new TimestampedScheduler();
        _Disposable = scheduler;

        scheduler.Run(period, (isFlag, canable) => 
        {
            IntPtr iconPtr = _hICon;
            if (!canable.IsDisposed)
            {
                var path = action?.Invoke(isFlag);
                if (!string.IsNullOrWhiteSpace(path))
                    iconPtr = User32.LoadImage(IntPtr.Zero, path, User32.ImageType.IMAGE_ICON, 32, 32, User32.LoadImageFlags.LR_LOADFROMFILE);
                else
                {
                    if (isFlag)
                        iconPtr = IntPtr.Zero;
                }

                var lastIcon = _NOTIFYICONDATA.hIcon;
                if (lastIcon != _hICon && lastIcon != IntPtr.Zero)
                    RuntimeInterop.DeleteObject(_hICon);
            }
            else
                _Disposable = null;

            _NOTIFYICONDATA.hIcon = iconPtr;
            RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Modify, ref _NOTIFYICONDATA);
        });
        
        return scheduler;
    }
}
