﻿using Maui.Toolkitx.Providers;

namespace Maui.Toolkitx;

internal partial class WindowChromeWorker : IProvider<IWindowChromeService>
{
    IWindowChromeService? IProvider<IWindowChromeService>.GetService() => _Service;
    object? IProvider.GetService(Type serviceType)
    {
        if (serviceType != typeof(IWindowChromeService))
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
