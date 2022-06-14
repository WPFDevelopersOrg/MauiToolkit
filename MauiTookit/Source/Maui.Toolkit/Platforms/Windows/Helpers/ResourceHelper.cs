using MicrosoftuiXaml = Microsoft.UI.Xaml;

namespace Maui.Toolkit.Platforms.Windows.Helpers;

internal class ResourceHelper
{
    static ResourceHelper()
    {
        __Resources = MicrosoftuiXaml.Application.Current.Resources;
    }

    readonly static MicrosoftuiXaml.ResourceDictionary __Resources;


    public static T? GetResource<T>(object key)
    {
        if (!__Resources.TryGetValue(key, out var value))
            return default;

        if (value is not T tValue)
            return default;

        return tValue;
    }

}
