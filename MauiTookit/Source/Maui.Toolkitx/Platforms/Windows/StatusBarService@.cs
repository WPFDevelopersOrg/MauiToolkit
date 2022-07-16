using Maui.Toolkitx.Concurrency;
using Maui.Toolkitx.Platforms.Windows.Runtimes;
using Maui.Toolkitx.Platforms.Windows.Runtimes.Shell32;
using PInvoke;

namespace Maui.Toolkitx;

internal partial class StatusBarService : IStatusBarService
{
    IDisposable IStatusBarService.Blink(TimeSpan period, Func<bool, string>? action)
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

    bool IStatusBarService.StopBlink()
    {
        _Disposable?.Dispose();
        _Disposable = default;

        return true;
    }



}
