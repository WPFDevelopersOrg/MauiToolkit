namespace Maui.Toolkitx.Disposables;
public class XActionDisposable : IDisposable
{
    volatile Action? _action;
    public XActionDisposable(Action action)
    {
        _action = action;
    }

    public void Dispose()
    {
        Interlocked.Exchange(ref _action, null)?.Invoke();
    }
}