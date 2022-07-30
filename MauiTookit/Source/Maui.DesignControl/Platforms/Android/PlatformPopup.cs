using Android.Content;
using Android.Runtime;
using Android.Util;
using platformview = Android.Views.View;

namespace Maui.DesignControl.Platforms.Android;
public class PlatformPopup : platformview
{
    public PlatformPopup(Context? context) : base(context)
    {
    }

    public PlatformPopup(Context? context, IAttributeSet? attrs) : base(context, attrs)
    {
    }

    public PlatformPopup(Context? context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
    {
    }

    public PlatformPopup(Context? context, IAttributeSet? attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
    {
    }

    protected PlatformPopup(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }
}
