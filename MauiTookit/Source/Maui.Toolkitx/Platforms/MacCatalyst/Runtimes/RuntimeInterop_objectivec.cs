using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using MapKit;
using ObjCRuntime;
using System.Runtime.InteropServices;

namespace Maui.Toolkitx.Platforms.MacCatalyst.Runtimes;

public static partial class RuntimeInterop
{
    private const string _ObjectiveCLibrary = Constants.ObjectiveCLibrary;

    /// <summary>
    /// 
    /// </summary>
	public struct objc_super
    {
        /// <summary>
        /// 
        /// </summary>
		public IntPtr Handle;
        /// <summary>
        /// 
        /// </summary>
		public IntPtr SuperHandle;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="arg1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public static extern IntPtr IntPtr_objc_msgSend_nfloat(IntPtr receiver, IntPtr selector, NFloat arg1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="arg1"></param>
    [DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public static extern void void_objc_msgSend_bool(IntPtr receiver, IntPtr selector, bool arg1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="scrollView"></param>
    /// <param name="velocity"></param>
    /// <param name="targetContentOffset"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_CGPoint_ref_CGPoint(IntPtr receiver, IntPtr selector, IntPtr scrollView, CGPoint velocity, ref CGPoint targetContentOffset);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSendSuper")]
    public extern static void void_objc_msgSendSuper(ref objc_super receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
	public static void void_objc_msgSend_string(IntPtr receiver, IntPtr selector, string value)
    {
        var ptr = CFString.CreateNative(value);
        void_objc_msgSend_IntPtr(receiver, selector, ptr);
        CFString.ReleaseNative(ptr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    [DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_nFloat(IntPtr receiver, IntPtr selector, NFloat value);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_ref_IntPtr(IntPtr receiver, IntPtr selector, ref IntPtr value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_out_IntPtr(IntPtr receiver, IntPtr selector, out IntPtr value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    [DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_CGRect_bool(IntPtr receiver, IntPtr selector, CGRect p1, bool p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_IntPtr_bool(IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2, bool p3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	public static void void_objc_msgSend_IntPtr_string_bool(IntPtr receiver, IntPtr selector, IntPtr p1, string p2, bool p3)
    {
        var ptr = CFString.CreateNative(p2);
        void_objc_msgSend_IntPtr_IntPtr_bool(receiver, selector, p1, ptr, p3);
        CFString.ReleaseNative(ptr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static nint nint_objc_msgSend_IntPtr_nint(IntPtr receiver, IntPtr selector, IntPtr p1, nint p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_ref_BlockLiteral(IntPtr receiver, IntPtr selector, IntPtr p1, ref BlockLiteral p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_IntPtr_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <param name="p5"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_IntPtr_IntPtr_NSRange_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2, IntPtr p3, NSRange p4, IntPtr p5);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_int(IntPtr receiver, IntPtr selector, int value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
    [DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_CGSize(IntPtr receiver, IntPtr selector, CGSize p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <param name="p5"></param>
    /// <param name="p6"></param>
    /// <param name="p7"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_int_int_int_int_int_int_IntPtr(IntPtr receiver, IntPtr selector, int p1, int p2, int p3, int p4, int p5, int p6, IntPtr p7);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <param name="p5"></param>
    /// <param name="p7"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_IntPtr_IntPtr_long_int_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2, IntPtr p3, long p4, int p5, IntPtr p7);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_long(IntPtr receiver, IntPtr selector, long value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_int_int_long(IntPtr receiver, IntPtr selector, int p1, int p2, long p3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_long_int_long(IntPtr receiver, IntPtr selector, long p1, int p2, long p3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_int(IntPtr receiver, IntPtr selector, int p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_long(IntPtr receiver, IntPtr selector, long p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static int int_objc_msgSend_int(IntPtr receiver, IntPtr selector, int p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_ref_IntPtr(IntPtr receiver, IntPtr selector, ref IntPtr p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    public static IntPtr IntPtr_objc_msgSend_string(IntPtr receiver, IntPtr selector, string value)
    {
        var ptr = CFString.CreateNative(value);
        var intPtr =  IntPtr_objc_msgSend_IntPtr(receiver, selector, ptr);
        CFString.ReleaseNative(ptr);

        return intPtr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_double_double(IntPtr receiver, IntPtr selector, double a, double b);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_bool(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.I1)] bool p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static double double_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static float float_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    [return: MarshalAs(UnmanagedType.I1)]
    public extern static bool bool_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static int int_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static long long_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static int int_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    [return: MarshalAs(UnmanagedType.I1)]
    public extern static bool bool_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    [return: MarshalAs(UnmanagedType.I1)]
    public extern static bool bool_objc_msgSend_IntPtr_int(IntPtr receiver, IntPtr selector, IntPtr p1, int p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static IntPtr IntPtr_objc_msgSend_CGSize(IntPtr receiver, IntPtr selector, CGSize p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGPoint CGPoint_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGSize CGSize_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_CGRect(IntPtr receiver, IntPtr selector, CGRect p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_int(IntPtr receiver, IntPtr selector, int p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr p1);

#if !__TVOS__
    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_MKCoordinateRegion_IntPtr(IntPtr receiver, IntPtr selector, MKCoordinateRegion p1, IntPtr p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_MKMapRect(IntPtr receiver, IntPtr selector, MKMapRect p1);
#endif // !__TVOS__

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_CGRect(IntPtr receiver, IntPtr selector, CGRect p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_CGRect_int(IntPtr receiver, IntPtr selector, CGRect p1, int p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_CGRect_IntPtr(IntPtr receiver, IntPtr selector, CGRect p1, IntPtr p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_CGRect_CGRect_float(IntPtr receiver, IntPtr selector, CGRect p1, CGRect p2, float p3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CGRect CGRect_objc_msgSend_CGRect_CGRect_CGRect(IntPtr receiver, IntPtr selector, CGRect p1, CGRect p2, CGRect p3);

#if !__WATCHOS__
#if !NET
		[DllImport (_ObjectiveCLibrary, EntryPoint="objc_msgSend")]
		public extern static Matrix3 Matrix3_objc_msgSend (IntPtr receiver, IntPtr selector);
#endif // !NET

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static CATransform3D CATransform3D_objc_msgSend(IntPtr receiver, IntPtr selector);
#endif // !__WATCHOS__

#if !__TVOS__

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_MKMapRect(out CGRect buf, IntPtr receiver, IntPtr selector, MKMapRect p1);
#endif // !__TVOS__

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret(out CGRect buf, IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGPoint_objc_msgSend_stret(out CGPoint buf, IntPtr receiver, IntPtr selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGSize_objc_msgSend_stret(out CGSize buf, IntPtr receiver, IntPtr selector);

#if !__WATCHOS__ && !NET
		[DllImport (_ObjectiveCLibrary, EntryPoint="objc_msgSend_stret")]
		public extern static void Matrix3_objc_msgSend_stret (out Matrix3 buf, IntPtr receiver, IntPtr selector);
#endif // !__WATCHOS__ && !NET

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_int(out CGRect buf, IntPtr receiver, IntPtr selector, int p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_IntPtr(out CGRect buf, IntPtr receiver, IntPtr selector, IntPtr p1);

#if !__TVOS__

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_MKCoordinateRegion_IntPtr(out CGRect buf, IntPtr receiver, IntPtr selector, MKCoordinateRegion p1, IntPtr p2);
#endif // !__TVOS__

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_CGRect(out CGRect buf, IntPtr receiver, IntPtr selector, CGRect p1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_CGRect_int(out CGRect buf, IntPtr receiver, IntPtr selector, CGRect p1, int p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_CGRect_IntPtr(out CGRect buf, IntPtr receiver, IntPtr selector, CGRect p1, IntPtr p2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_CGRect_CGRect_float(out CGRect buf, IntPtr receiver, IntPtr selector, CGRect p1, CGRect p2, float p3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CGRect_objc_msgSend_stret_CGRect_CGRect_CGRect(out CGRect buf, IntPtr receiver, IntPtr selector, CGRect p1, CGRect p2, CGRect p3);

#if !__WATCHOS__

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
    public extern static void CATransform3D_objc_msgSend_stret(out CATransform3D buf, IntPtr receiver, IntPtr selector);
#endif // !__WATCHOS__

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_int_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, int p1, ref IntPtr p2, out IntPtr p3);

#if NET

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_int_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, int p1, ref NativeHandle p2, out NativeHandle p3);
#endif

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_int_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, int p1, IntPtr p2, IntPtr p3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_int_int_int(IntPtr receiver, IntPtr selector, int p1, ref int p2, out int p3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <param name="p5"></param>
    /// <param name="p6"></param>
    /// <param name="p7"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_IntPtr_IntPtr_IntPtr_IntPtr_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, ref IntPtr p1, ref IntPtr p2, ref IntPtr p3, ref IntPtr p4, ref IntPtr p5, ref IntPtr p6, ref IntPtr p7);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="selector"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
	[DllImport(_ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
    public extern static void void_objc_msgSend_IntPtr_IntPtr_BlockLiteral(IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2, ref BlockLiteral p3);
}

