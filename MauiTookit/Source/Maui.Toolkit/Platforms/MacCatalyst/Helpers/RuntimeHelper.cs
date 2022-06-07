using CoreGraphics;
using Foundation;
using Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
using ObjCRuntime;
using System.Runtime.InteropServices;

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

    public static bool Dealloc(this NSObject nsObject)
    {
        if (nsObject is null)
            return false;

        var deAllocSelector = new Selector("dealloc");
        RuntimeInterop.void_objc_msgSend(nsObject.Handle, deAllocSelector.Handle);

        return true;
    }

    public static NSObject? GetNsObjectFrom(this NSObject nsObject, string name)
    {
        if (nsObject is null)
            return default;

        var propertySelector = new Selector(name);
        if (!nsObject.RespondsToSelector(propertySelector))
            return default;

        return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend(nsObject.Handle, propertySelector.Handle));
    }

    public static NSObject? GetNSObjectFromWithArgument<T>(this NSObject nsObject, string name, T argument)
    {
        if (nsObject is null)
            return default;

        var propertySelector = new Selector(name);
        if (!nsObject.RespondsToSelector(propertySelector))
            return default;

        if (argument is NFloat nFloat)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_nfloat(nsObject.Handle, propertySelector.Handle, nFloat));
        else if (argument is int intValue)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_int(nsObject.Handle, propertySelector.Handle, intValue));
        else if (argument is long longValue)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_long(nsObject.Handle, propertySelector.Handle, longValue));
        else if (argument is IntPtr intPtrValue)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_IntPtr(nsObject.Handle, propertySelector.Handle, intPtrValue));
        else if (argument is bool boolValue)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_bool(nsObject.Handle, propertySelector.Handle, boolValue));
        else if (argument is CGSize vcgSizeValue)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_CGSize(nsObject.Handle, propertySelector.Handle, vcgSizeValue));
        else if(argument is string stringValue)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_string(nsObject.Handle, propertySelector.Handle, stringValue));
        else
            return default;
    }

    public static bool SetValueForNsobject<T>(this NSObject nsObject, string name, T value)
    {
        if (nsObject is null)
            return default;

        var propertySelector = new Selector(name);
        if (!nsObject.RespondsToSelector(propertySelector))
            return default;

        if (value is bool boolValue)
            RuntimeInterop.void_objc_msgSend_bool(nsObject.Handle, propertySelector.Handle, boolValue);
        else if (value is IntPtr intPtrValue)
            RuntimeInterop.void_objc_msgSend_IntPtr(nsObject.Handle, propertySelector.Handle, intPtrValue);
        else if (value is int intValue)
            RuntimeInterop.void_objc_msgSend_int(nsObject.Handle, propertySelector.Handle, intValue);
        else if (value is long longValue)
            RuntimeInterop.void_objc_msgSend_long(nsObject.Handle, propertySelector.Handle, longValue);
        else if (value is CGSize cgSizeValue)
            RuntimeInterop.void_objc_msgSend_CGSize(nsObject.Handle, propertySelector.Handle, cgSizeValue);
        else if (value is string stringValue)
            RuntimeInterop.void_objc_msgSend_string(nsObject.Handle, propertySelector.Handle, stringValue);
        else
            return false;

        return true;
    }

}
