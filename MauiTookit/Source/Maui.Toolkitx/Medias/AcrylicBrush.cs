﻿using Maui.Toolkitx.Assists.Compositions;

namespace Maui.Toolkitx.Medias;
public class AcrylicBrush : Brush
{
    public override bool IsEmpty => throw new NotImplementedException();

    public static readonly BindableProperty TintColorProperty =
                           BindableProperty.Create(propertyName: nameof(TintColor),
                                                 returnType: typeof(Color),
                                                 declaringType: typeof(AcrylicBrush),
                                                 defaultValue: Colors.Black,
                                                 propertyChanged: OnProperyChanged);


    public static readonly BindableProperty TintOpacityProperty =
                           BindableProperty.Create(propertyName: nameof(TintOpacity),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(AcrylicBrush),
                                                   defaultValue: 0.5d,
                                                   propertyChanged: OnProperyChanged);


    public static readonly BindableProperty TintLuminosityOpacityProperty =
                           BindableProperty.Create(propertyName: nameof(TintLuminosityOpacity),
                                                   returnType: typeof(double),
                                                   declaringType: typeof(AcrylicBrush),
                                                   defaultValue: default,
                                                   propertyChanged: OnProperyChanged);

    public static readonly BindableProperty FallbackColorProperty =
                           BindableProperty.Create(propertyName: nameof(FallbackColor),
                                                   returnType: typeof(Color),
                                                   declaringType: typeof(AcrylicBrush),
                                                   defaultValue: Colors.Green,
                                                   propertyChanged: OnProperyChanged);


    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    public double TintOpacity
    {
        get => (double)GetValue(TintOpacityProperty);
        set => SetValue(TintOpacityProperty, value);
    }

    public double? TintLuminosityOpacity
    {
        get => (double?)GetValue(TintLuminosityOpacityProperty);
        set => SetValue(TintLuminosityOpacityProperty, value);
    }

    public Color FallbackColor
    {
        get => (Color)GetValue(FallbackColorProperty);
        set => SetValue(FallbackColorProperty, value);
    }


    private static void OnProperyChanged(BindableObject bindable, object oldValue, object newValue)
    {
         
    }
}
