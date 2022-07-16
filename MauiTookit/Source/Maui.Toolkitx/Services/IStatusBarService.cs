namespace Maui.Toolkitx.Services;
public interface IStatusBarService
{
    IDisposable Blink(TimeSpan period, Func<bool, string>? action = default);
    bool StopBlink();
}
