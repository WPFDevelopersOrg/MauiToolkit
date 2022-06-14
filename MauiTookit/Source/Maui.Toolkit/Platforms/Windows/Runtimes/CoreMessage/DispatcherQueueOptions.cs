namespace Maui.Toolkit.Platforms.Windows.Runtimes.CoreMessage;

[StructLayout(LayoutKind.Sequential)]
public struct DispatcherQueueOptions
{
    public int dwSize;
    public int threadType;
    public int apartmentType;
}
