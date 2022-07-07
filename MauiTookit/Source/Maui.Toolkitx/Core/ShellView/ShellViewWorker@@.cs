using Maui.Toolkitx.Providers;

namespace Maui.Toolkitx.Core;

internal partial class ShellViewWorker : IProvider<IShellViewService>
{
    IShellViewService? IProvider<IShellViewService>.GetService()
    {
        throw new NotImplementedException();
    }

    object? IProvider.GetService(Type serviceType)
    {
        throw new NotImplementedException();
    }

    public T? GetService<T>()
    {
        throw new NotImplementedException();
    }
}
