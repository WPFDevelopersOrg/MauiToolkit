using CoreGraphics;
using Foundation;
using Maui.Toolkitx.Platforms.MacCatalyst.Runtimes;
using ObjCRuntime;
using System.Runtime.InteropServices;

namespace Maui.Toolkitx.Platforms.MacCatalyst.Helpers;

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
        else if (argument is string stringValue)
            return Runtime.GetNSObject(RuntimeInterop.IntPtr_objc_msgSend_string(nsObject.Handle, propertySelector.Handle, stringValue));
        else
            return default;
    }

    public static TValue? GetValueFromNsobject<TValue>(this NSObject nsObject, string name)
    {
        if (nsObject is null)
            return default;

        var propertySelector = new Selector(name);
        if (!nsObject.RespondsToSelector(propertySelector))
            return default;

        if (typeof(TValue) == typeof(int))
        {
            int value = RuntimeInterop.int_objc_msgSend(nsObject.Handle, propertySelector.Handle);
            if (value is TValue intValue)
                return intValue;
        }
        else if (typeof(TValue) == typeof(bool))
        {
            bool value = RuntimeInterop.bool_objc_msgSend(nsObject.Handle, propertySelector.Handle);
            if (value is TValue boolValue)
                return boolValue;
        }
        else if (typeof(TValue) == typeof(float))
        {
            float value = RuntimeInterop.float_objc_msgSend(nsObject.Handle, propertySelector.Handle);
            if (value is TValue floatValue)
                return floatValue;
        }
        else if (typeof(TValue) == typeof(double))
        {
            double value = RuntimeInterop.double_objc_msgSend(nsObject.Handle, propertySelector.Handle);
            if (value is TValue doubleValue)
                return doubleValue;
        }
        else if (typeof(TValue) == typeof(long))
        {
            long value = RuntimeInterop.long_objc_msgSend(nsObject.Handle, propertySelector.Handle);
            if (value is TValue longValue)
                return longValue;
        }
        else if (typeof(TValue) == typeof(ulong))
        {
            ulong value = (ulong)RuntimeInterop.long_objc_msgSend(nsObject.Handle, propertySelector.Handle);
            if (value is TValue longValue)
                return longValue;
        }
        else if(typeof(TValue) == typeof(IntPtr))
        {
            IntPtr value = RuntimeInterop.IntPtr_objc_msgSend(nsObject.Handle, propertySelector.Handle);
            if (value is TValue intPtrValue)
                return intPtrValue;
        }
        
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
        else if (value is ulong ulongValue)
            RuntimeInterop.void_objc_msgSend_long(nsObject.Handle, propertySelector.Handle, (long)ulongValue);
        else if (value is CGSize cgSizeValue)
            RuntimeInterop.void_objc_msgSend_CGSize(nsObject.Handle, propertySelector.Handle, cgSizeValue);
        else if (value is string stringValue)
            RuntimeInterop.void_objc_msgSend_string(nsObject.Handle, propertySelector.Handle, stringValue);
        else if(value is NFloat nFloat)
            RuntimeInterop.void_objc_msgSend_nFloat(nsObject.Handle, propertySelector.Handle, nFloat);
        else
            return false;

        return true;
    }

    public static bool SetValueForNsobject<TArg1, TArg2>(this NSObject nsObject, string name, TArg1 arg1, TArg2 arg2)
    {
        if (nsObject is null)
            return default;

        var propertySelector = new Selector(name);
        if (!nsObject.RespondsToSelector(propertySelector))
            return default;

        if (arg1 is CGRect cgRectValue && arg2 is bool boolValue)
            RuntimeInterop.void_objc_msgSend_CGRect_bool(nsObject.Handle, propertySelector.Handle, cgRectValue, boolValue);
        else if (arg1 is IntPtr intPtrValue1 && arg2 is IntPtr intPtrValue2)
            RuntimeInterop.void_objc_msgSend_IntPtr_IntPtr(nsObject.Handle, propertySelector.Handle, intPtrValue1, intPtrValue2);
        else
            return false;

        return true;
    }

    public static bool ExecuteMethod(this NSObject nsObject, string name)
    {
        if (nsObject is null)
            return default;

        var propertySelector = new Selector(name);
        if (!nsObject.RespondsToSelector(propertySelector))
            return default;

        RuntimeInterop.void_objc_msgSend(nsObject.Handle, propertySelector.Handle);
        return true;
    }
}
