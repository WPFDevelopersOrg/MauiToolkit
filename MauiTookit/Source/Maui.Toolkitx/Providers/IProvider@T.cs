namespace Maui.Toolkitx.Providers;

public interface IProvider<T> : IProvider
{
    T GetService();
}
