
namespace Maui.Toolkit.Extensions;
public static class HashTableExtensions
{
    public static TValue? GetDefaultOrValue<TValue>(this Hashtable hashtable, object key)
    {
        if (hashtable is null)
            return default;

        if (!hashtable.ContainsKey(key))
            return default;

        if (hashtable[key] is TValue ttValue)
            return ttValue;

        return default;
    }


    public static TValue? RemoveFrom<TValue>(this Hashtable hashtable, object key)
    {
        if (hashtable is null)
            return default;

        if (!hashtable.ContainsKey(key))
            return default;

        if (hashtable[key] is TValue ttValue)
        {
            hashtable.Remove(key);
            return ttValue;
        }

        return default;
    }

}
