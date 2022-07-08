namespace Maui.Toolkitx.Config;

public class StatusBarConfigurations
{
    /// <summary>
    /// show title default it is the app name
    /// the tookit default value is  AppName
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// show tooltip default ti is the app name 
    /// the tookit default value is  AppName
    /// </summary>
    public string? ToolTipText { get; set; }

    /// <summary>
    /// the icon path 
    /// </summary>
    public string? Icon1 { get; set; }

    /// <summary>
    /// when start blink it will use icon2
    /// </summary>
    public string? Icon2 { get; set; }
}
