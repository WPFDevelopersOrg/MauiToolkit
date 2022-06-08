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

    public static string GetApplicationName()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var appName = entryAssembly?.GetName().Name;
        if (!string.IsNullOrWhiteSpace(appName))
            return appName;

        appName = Application.Current?.Windows?.FirstOrDefault()?.Title;
        if (!string.IsNullOrWhiteSpace(appName))
            return appName;

        //this is null when in ios
#if WINDOWS || MACCATALYST 
        appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
#else
        appName = "Application";
#endif

        return appName;
    }

}


