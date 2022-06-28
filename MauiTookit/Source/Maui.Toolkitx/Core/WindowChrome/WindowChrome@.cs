namespace Maui.Toolkitx;

public partial class WindowChrome : BindableObject
{
    public static readonly BindableProperty CaptionHeightProperty = 
                           BindableProperty.Create(propertyName: nameof(CaptionHeight),
                                                   returnType:typeof(double),
                                                   declaringType:typeof(WindowChrome),
                                                   defaultValue:default,
                                                   propertyChanged: OnCaptionHeightChanged);


    public static readonly BindableProperty FullScreenProperty =
                           BindableProperty.Create(propertyName: nameof(FullScreen),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnFullScreenChanged);

    public static readonly BindableProperty RemoveNavigationTitlebarProperty =
                           BindableProperty.Create(propertyName: nameof(RemoveNavigationTitlebar),
                                                   returnType: typeof(bool),
                                                   declaringType: typeof(WindowChrome),
                                                   defaultValue: default,
                                                   propertyChanged: OnRemoveNavigationTitlebarChanged);



    public double CaptionHeight
    {
        get => (double)GetValue(CaptionHeightProperty);
        set => SetValue(CaptionHeightProperty, value);
    }

    public bool FullScreen
    {
        get => (bool)GetValue(FullScreenProperty);
        set => SetValue(FullScreenProperty, value);
    }

    public bool RemoveNavigationTitlebar
    {
        get => (bool)GetValue(RemoveNavigationTitlebarProperty);
        set => SetValue(RemoveNavigationTitlebarProperty, value);
    }

    private static void OnCaptionHeightChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //var windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(bindable);

    }

    private static void OnFullScreenChanged(BindableObject bindable, object oldValue, object newValue)
    {
         
    }

    private static void OnRemoveNavigationTitlebarChanged(BindableObject bindable, object oldValue, object newValue)
    {
        
    }
}
