using Maui.Toolkit.Options;
using Maui.Toolkit.Platforms.Windows.Runtimes;
using Maui.Toolkit.Platforms.Windows.Runtimes.Shell32;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using PInvoke;
using TimerX = System.Timers.Timer;


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
    volatile bool _IsShowIn = false;

    volatile bool _IsBlinking = false;
    TimerX? _BlinkTimer;
    object _BlinkLock = new();

    IntPtr _hICon;
    volatile bool _IsReversed = false;


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

    bool IStatusBarService.Blink(double rate)
    {
        if (_MainWindow is null)
            return false;

        lock (_BlinkLock)
        {
            if (_IsBlinking)
                return true;

            if (rate <= 0)
                rate = 500;
            else if (rate > 3000)
                rate = 3000;

            if (_BlinkTimer is null)
            {
                _BlinkTimer = new TimerX(rate);
                _BlinkTimer.Elapsed += BlinkTimer_Elapsed;
            }
            else
                _BlinkTimer.Interval = rate;

            _BlinkTimer.Start();
            _IsBlinking = true;
        }

        return true;
    }

    bool IStatusBarService.Hide()
    {
        lock (this)
        {
            _IsShowIn = false;
            ((IStatusBarService)this).Stop();
            return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Delete, ref _NOTIFYICONDATA);
        }
    }

    bool IStatusBarService.SetDescription(string? text) => true;

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

            if (_IsShowIn)
                return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Modify, ref _NOTIFYICONDATA);
            else
            {
                _IsShowIn = true;
                return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Add, ref _NOTIFYICONDATA);
            }
        }
    }

    bool IStatusBarService.Stop()
    {
        lock (_BlinkLock)
        {
            if (!_IsBlinking)
                return true;

            _IsBlinking = false;
            if (_BlinkTimer is null)
                return true;

            _BlinkTimer.Stop();
            _BlinkTimer.Elapsed -= BlinkTimer_Elapsed;
            _BlinkTimer = null;
        }

        return true;
    }

    void BlinkTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        lock (this)
        {
            if (_IsReversed)
            {
                _IsReversed = false;
                _NOTIFYICONDATA.hIcon = IntPtr.Zero;
                RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Modify, ref _NOTIFYICONDATA);
            }
            else
            {
                _IsReversed = true;
                _NOTIFYICONDATA.hIcon = _hICon;
                RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Modify, ref _NOTIFYICONDATA);
            }
        }
    }
}
