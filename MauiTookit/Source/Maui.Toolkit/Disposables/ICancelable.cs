namespace Maui.Toolkit.Disposables;

public interface ICancelable : IDisposable
{
    /// <summary>
    /// Gets a value that indicates whether the object is disposed.
    /// </summary>
    bool IsDisposed { get; }
}