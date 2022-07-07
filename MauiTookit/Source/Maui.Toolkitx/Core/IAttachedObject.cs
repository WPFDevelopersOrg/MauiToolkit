namespace Maui.Toolkitx.Core;

public interface IAttachedObject
{
    bool IsAttached { get; }
    BindableObject? AssociatedObject { get; }
    void Attach(BindableObject bindableObject);
    void Detach();
}
