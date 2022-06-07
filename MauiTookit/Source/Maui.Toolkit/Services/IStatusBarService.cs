using Maui.Toolkit.Disposables;

namespace Maui.Toolkit.Services;
public interface IStatusBarService
{
    /// <summary>
    /// show StatusBar you can set different icon 
    /// </summary>
    /// <param name="iconPath"></param>
    /// <returns></returns>
    bool Show(string? iconPath = default);

    /// <summary>
    /// hide Statuebar
    /// </summary>
    /// <returns></returns>
    bool Hide();

    /// <summary>
    /// when you need prompt users you can blink the icon
    /// </summary>
    /// <param name="period"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    IDisposable SchedulePeriodic(TimeSpan period, Func<bool, string>? action);

    /// <summary>
    /// this is the tip text when you use mac os
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    bool SetDescription(string? text);

    /// <summary>
    /// when click the statuebar or other operate will trigger this event
    /// </summary>
    event EventHandler<EventArgs> StatusBarEventChanged;
}
