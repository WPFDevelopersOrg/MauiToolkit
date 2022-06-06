using Maui.Toolkit.Services;

namespace Maui.Toolkit.Platforms;

internal class StatusBarServiceImp : IStatusBarService
{

    private event EventHandler<EventArgs>? StatusBarEventChanged;

    event EventHandler<EventArgs> IStatusBarService.StatusBarEventChanged
    {
        add => StatusBarEventChanged += value;
        remove => StatusBarEventChanged -= value;
    }

    bool IStatusBarService.Blink(double rate)
    {
        throw new NotImplementedException();
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

    bool IStatusBarService.Stop()
    {
        throw new NotImplementedException();
    }
}
