using Maui.Toolkit.Disposables;
using TimerX = System.Timers.Timer;


namespace Maui.Toolkit.Concurrency;

public class TimestampedScheduler : ICancelable
{
    public TimestampedScheduler()
    {
        _Timer = new TimerX();
        _Timer.Elapsed += Timer_Elapsed;
    }

    readonly TimerX _Timer;
    Action<bool, ICancelable>? _Action;
    bool _Running = false;
    bool _IsReversed = false;

    public bool Run(TimeSpan span, Action<bool, ICancelable> action)
    {
        if (_Running)
            return true;

        _Action = action;   
        _Timer.Interval = span.TotalMilliseconds;
        _Timer.Start();
        _Running = true;

        return true;
    }

    private bool disposedValue;

    bool ICancelable.IsDisposed => disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _Timer.Stop();
                _Timer.Elapsed -= Timer_Elapsed;
            }

            disposedValue = true;
        }
    }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);

        lock (this)
            _Action?.Invoke(false, this);
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        lock (this)
        {
            _Action?.Invoke(_IsReversed, this);
            _IsReversed = !_IsReversed;
        }
    }
}
