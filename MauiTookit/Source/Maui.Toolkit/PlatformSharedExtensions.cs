using Maui.Toolkit.Builders;

namespace Maui.Toolkit;
public static class PlatformSharedExtensions
{
    public static FilePathBuilder AddBasicDirectory(this FilePathBuilder builder, string basicDirectory)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.SetBasicDirectory(basicDirectory);
        return builder;
    }

    public static FilePathBuilder AddArgument(this FilePathBuilder builder, string argument)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.AddNodeName(argument);
        return builder;
    }
}
