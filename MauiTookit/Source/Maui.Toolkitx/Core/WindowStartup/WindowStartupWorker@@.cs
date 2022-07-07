﻿using Maui.Toolkitx.Providers;

namespace Maui.Toolkitx;
internal partial class WindowStartupWorker : IProvider<IWindowStartupService>
{
    IWindowStartupService? IProvider<IWindowStartupService>.GetService() => _Service;

    object? IProvider.GetService(Type serviceType)
    {
        if (serviceType != typeof(IWindowStartupService))
            return default;

        return _Service;
    }

    public T? GetService<T>()
    {
        if (_Service is T tValue)
            return tValue;

        return default;
    }
}
