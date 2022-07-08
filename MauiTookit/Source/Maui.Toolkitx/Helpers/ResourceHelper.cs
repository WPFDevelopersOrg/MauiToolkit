namespace Maui.Toolkitx.Helpers;

public class ResourceHelper
{
    public static T? GetResource<T>(string key)
    {
        if (Application.Current?.Resources.TryGetValue(key, out var value) != true)
            return default;

        if (value is not T tValue)
            return default;

        return tValue;
    }


}