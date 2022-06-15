using Maui.Toolkit.Disposables;
using Maui.Toolkit.Options;
using Maui.Toolkit.Services;

namespace Maui.Toolkit.Platforms;

internal class StatusBarServiceImp : IStatusBarService
{
    public StatusBarServiceImp(StatusBarOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _StatusBarOptions = options;
    }

    readonly StatusBarOptions _StatusBarOptions;

    private event EventHandler<EventArgs>? StatusBarEventChanged;

    event EventHandler<EventArgs> IStatusBarService.StatusBarEventChanged
    {
        add => StatusBarEventChanged += value;
        remove => StatusBarEventChanged -= value;
    }

    public bool RegisterApplicationEvent(ILifecycleBuilder lifecycleBuilder)
    {
        lifecycleBuilder.AddiOS(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {


            }).OnResignActivation(app =>
            {

            }).ContinueUserActivity((app, user, handler) =>
            {

                return true;

            }).DidEnterBackground(app =>
            {

            }).WillFinishLaunching((app, options) =>
            {
                return true;
            }).FinishedLaunching((app, options) =>
            {
                return true;
            }).OpenUrl((app, url, options) =>
            {
                return true;
            }).PerformActionForShortcutItem((app, item, handler) =>
            {

            }).WillEnterForeground(app =>
            {

            }).WillTerminate(app =>
            {

            }).SceneWillConnect((scrne, session, options) =>
            {

            }).SceneDidDisconnect(scene =>
            {

            });
        });

        return true;
    }

    bool IStatusBarService.Hide()
    {
        return true;
    }

    bool IStatusBarService.SetDescription(string? text)
    {
        return true;
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        return true;
    }

    IDisposable IStatusBarService.SchedulePeriodic(TimeSpan period, Func<bool, string>? action)
    {
        StatusBarEventChanged?.Invoke(this, EventArgs.Empty);
        return new NullDisposable();
    }
}
