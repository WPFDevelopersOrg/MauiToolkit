using Maui.Toolkit.Builders;

namespace Maui.Toolkit;
public class PlatformShared
{
    public static FilePathBuilder CreatePathBuilder()
    {
        var builder = new FilePathBuilder();
        builder.SetBasicDirectory();
        return builder;
    }



}


