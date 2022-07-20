using ObjCRuntime;

namespace Maui.Toolkit.Platforms.MacCatalyst.Runtimes;
internal static class Libraries
{
    public static class AppKit
    {
        public static readonly IntPtr Handle = Dlfcn.dlopen(Constants.AppKitLibrary, 0);
    }
}
