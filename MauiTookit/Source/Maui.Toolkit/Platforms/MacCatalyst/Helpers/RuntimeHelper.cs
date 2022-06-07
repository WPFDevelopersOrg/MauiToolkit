using Foundation;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using ObjCRuntime;
using System;
namespace Maui.Toolkit.Platforms.MacCatalyst.Helpers;

public static class RuntimeHelper
{
    public static NSObject? Alloc(string name)
    {
        var allocSelector = new Selector("alloc");
        var intPtr = RuntimeInterop.IntPtr_objc_msgSend(Class.GetHandle(name), allocSelector.Handle);
        if (intPtr == IntPtr.Zero)
            return default;

        return Runtime.GetNSObject(intPtr);
    }
}
