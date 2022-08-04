using MauiAppZx.Views;

namespace MauiAppZx;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Loaded += AppShell_Loaded;
        //var xxxx = Application.Current?.Resources["123"];

        //Application.Current?.Resources.TryGetValue("Primary", out var color);

        //Application.Current.OpenWindow();

        Routing.RegisterRoute("Login", typeof(Login));

    }

    private async void AppShell_Loaded(object? sender, EventArgs e)
    {
        await GoToAsync("Login", true);
    }
}
