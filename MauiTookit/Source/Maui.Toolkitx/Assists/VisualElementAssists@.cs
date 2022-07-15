using Maui.Toolkitx.Assists.Compositions;

namespace Maui.Toolkitx.Assists;
public partial class VisualElementAssists : IAttachedObject
{
    bool _IsAttached = false;

    Window? _AssociatedObject;
    BindableObject? IAttachedObject.AssociatedObject => _AssociatedObject;
    bool IAttachedObject.IsAttached => _IsAttached;

    public void Attach(BindableObject bindableObject)
    {
         
    }

    public void Detach()
    {
         
    }
}
