namespace Maui.Toolkitx.Monitors;

public class BackdropMonitor : IOptionsMonitor<BackdropsKind>
{
    BackdropsKind IOptionsMonitor<BackdropsKind>.CurrentValue => throw new NotImplementedException();

    BackdropsKind IOptionsMonitor<BackdropsKind>.Get(string name)
    {
        throw new NotImplementedException();
    }

    IDisposable IOptionsMonitor<BackdropsKind>.OnChange(Action<BackdropsKind, string> listener)
    {
        throw new NotImplementedException();
    }
}
