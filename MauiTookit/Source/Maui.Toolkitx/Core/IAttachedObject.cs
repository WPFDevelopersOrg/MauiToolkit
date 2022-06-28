namespace Maui.Toolkitx.Core;

public interface IAttachedObject
{
    BindableObject? AssociatedObject { get; }
    void Attach(BindableObject bindableObject);
    void Detach();
}
