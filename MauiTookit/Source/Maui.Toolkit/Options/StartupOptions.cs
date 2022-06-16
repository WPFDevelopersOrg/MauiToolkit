using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Options;
public class StartupOptions
{
    /// <summary>
    /// You can use Mica Acrylic in Windows
    /// You can use vibrancy in macos
    /// </summary>
    public BackdropsKind BackdropsKind { get; set; }

    /// <summary>
    /// hide titlebar or not 
    /// the tookit default value is ExtendsContentIntoTitleBar
    /// </summary>
    public WindowTitleBarKind TitleBarKind { get; set; }

    /// <summary>
    /// Only TitleBarKind is default can take effect
    /// </summary>
    public Color? TitleBarBackgroundColor { get; set; }

    /// <summary>
    /// Only TitleBarKind is default can take effect
    /// </summary>
    public Color? TitleBarBackgroundInactiveColor { get; set; }

    /// <summary>
    /// Only TitleBarKind is default can take effect
    /// </summary>
    public Color? TitleBarForegroundColor { get; set; }

    /// <summary>
    /// Only TitleBarKind is default can take effect
    /// </summary>
    public Color? TitleBarForegroundInactiveColor { get; set; }

    /// <summary>
    /// fullscreen or Max Min
    /// the tookit default value is  Maximize
    /// </summary>
    public WindowPresenterKind PresenterKind { get; set; }

    /// <summary>
    /// center or not
    /// the tookit default value is  center
    /// </summary>
    public WindowAlignment Location { get; set; }

    /// <summary>
    /// if the window is not max min or fullscreen the size will use
    /// the tookit default value is  (1000,500)
    /// </summary>
    public Size Size { get; set; }
}
