using Microsoftui = Microsoft.UI;
using MicrosoftuiXaml = Microsoft.UI.Xaml;
using MicrosoftuixamlMedia = Microsoft.UI.Xaml.Media;
using Windowsui = Windows.UI;

namespace Maui.Toolkitx.Platforms.Windows.Extensions;
public static class PlatformExtensions
{
    public static Windowsui.Color ToPlatformColor(this Color color)
    {
        if (color is null)
            return Microsoftui.Colors.Transparent;

        color.ToRgba(out var red, out var green, out var blue, out var alpha);
        return Windowsui.Color.FromArgb(alpha, red, green, blue);
    }

    public static MicrosoftuixamlMedia.Brush ToPlatformBrush(this Color color) => new MicrosoftuixamlMedia.SolidColorBrush(color.ToPlatformColor());


    public static MicrosoftuiXaml.Thickness ToPlatformThickness(this Thickness thickness) => new MicrosoftuiXaml.Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);

}
