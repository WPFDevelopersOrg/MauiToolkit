using Winui = Windows.UI;
using Microsoftui = Microsoft.UI;
using WinuiMedia = Microsoft.UI.Xaml.Media;
using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkit.Platforms.Windows.Extensions;

public static class PlatformExtensions
{
    public static Winui.Color MauiColor2WinuiColor(this Color color)
    {
        if (color is null)
            return Microsoftui.Colors.Transparent;

        color.ToRgba(out var red, out var green, out var blue, out var alpha);
        return Winui.Color.FromArgb(alpha, red, green, blue);
    }

    public static WinuiMedia.Brush MauiColor2WinuiBrush(this Color color) =>  new WinuiMedia.SolidColorBrush(color.MauiColor2WinuiColor());


    public static MicrosoftuiXaml.Thickness MauiThickness2WinuiThickness(this Thickness thickness) => new MicrosoftuiXaml.Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
}
