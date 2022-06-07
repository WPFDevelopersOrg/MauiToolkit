﻿using Maui.Toolkit.Disposables;
using Maui.Toolkit.Options;
using Maui.Toolkit.Services;
using Microsoft.Maui.LifecycleEvents;

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
        lifecycleBuilder.AddMac(windowsLeftCycle =>
        {
            windowsLeftCycle.OnActivated(app =>
            {
                //Show(_Options.IconFilePath);

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
        throw new NotImplementedException();
    }

    bool IStatusBarService.SetDescription(string? text)
    {
        throw new NotImplementedException();
    }

    bool IStatusBarService.Show(string? iconPath)
    {
        throw new NotImplementedException();
    }

    IDisposable IStatusBarService.SchedulePeriodic(TimeSpan period, Func<bool, string>? action)
    {
        throw new NotImplementedException();
    }
}
