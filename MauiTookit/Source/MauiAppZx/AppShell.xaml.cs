namespace MauiAppZx;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        //var xxxx = Application.Current?.Resources["123"];

       Application.Current?.Resources.TryGetValue("Primary", out var color);
    }
}
