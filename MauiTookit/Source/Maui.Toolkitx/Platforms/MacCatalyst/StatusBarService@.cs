using Maui.Toolkitx.Concurrency;

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
            string? path = _Config.Icon1;
            if (!canable.IsDisposed)
            {
                if (isFlag)
                    path = action?.Invoke(isFlag);
            }
            else
                _Disposable = null;

            SetImage(path);
        });

        return scheduler;
    }

    bool IStatusBarService.StopBlink()
    {
        _Disposable?.Dispose();
        return true;
    }
}
