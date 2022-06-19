using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Options;
public class ShellViewOptions
{
    /// <summary>
    /// this is title 
    /// </summary>
    public string? Title;

    /// <summary>
    /// this is app icon file path 
    /// </summary>
    public string? Icon;

    /// <summary>
    /// TitleBarHeight
    /// </summary>
    public double TitleBarHeight;

    /// <summary>
    /// FontSize
    /// </summary>
    public double TitleFontSize;

    /// <summary>
    /// can show or hide togglebutton
    /// </summary>
    public bool IsPaneToggleButtonVisible;

    /// <summary>
    /// can show or hide the  back button
    /// </summary>
    public VisibilityKind BackButtonVisible;

    /// <summary>
    /// can show or  hide settings button
    /// </summary>
    public bool IsSettingsVisible;

    public double SettingsHeight;

    public Thickness SettingsMargin;

    /// <summary>
    /// can show or hide searchbar 
    /// </summary>
    public bool IsSerchBarVisible;

    public string? SearchaBarPlaceholderText;

    /// <summary>
    /// this is all window color
    /// </summary>
    public Color? BackgroundColor;

    /// <summary>
    /// this is all window brush
    /// </summary>
    public Brush? Background;

    /// <summary>
    /// this is only frame color
    /// </summary>
    public Color? ContentBackgroundColor;

    /// <summary>
    /// this is only frame brush
    /// </summary>
    public Brush? ContentBackground;
}
