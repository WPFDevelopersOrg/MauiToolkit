using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Toolkitx;
internal partial class WindowStartupWorker : IAttachedObject
{
    BindableObject? IAttachedObject.AssociatedObject => throw new NotImplementedException();

    void IAttachedObject.Attach(BindableObject bindableObject)
    {
        throw new NotImplementedException();
    }

    void IAttachedObject.Detach()
    {
        throw new NotImplementedException();
    }
}
