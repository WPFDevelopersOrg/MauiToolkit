using MauiAppx.Views;

namespace MauiAppx;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("LoginRouter", typeof(LoginPage));
    }
}
