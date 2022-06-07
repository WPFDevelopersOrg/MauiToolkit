using Maui.Toolkit.Disposables;

namespace Maui.Toolkit.Concurrency;

public interface ISchedulerLongRunning
{
    IDisposable ScheduleLongRunning<TState>(TState state, Action<TState, ICancelable> action);
}
