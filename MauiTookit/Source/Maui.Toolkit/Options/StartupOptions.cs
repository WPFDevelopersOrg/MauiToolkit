using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Options;
public class StartupOptions
{
    /// <summary>
    /// hide titlebar or not
    /// </summary>
    public WindowTitleBarKind TitleBarKind { get; set; }

    /// <summary>
    /// fullscreen or Max Min
    /// </summary>
    public WindowPresenterKind PresenterKind { get; set; }

    /// <summary>
    /// center or not
    /// </summary>
    public WidnowAlignment Location { get; set; }

    /// <summary>
    /// if the window is not max min or fullscreen the size will use
    /// </summary>
    public Size Size { get; set; }
}
