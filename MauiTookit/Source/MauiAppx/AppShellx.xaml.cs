using Maui.Toolkit.Helpers;
using MauiAppx.Views;

namespace MauiAppx;

public partial class AppShellx : Shell
{
    public AppShellx()
    {
        InitializeComponent();

        Routing.RegisterRoute("LoginRouter", typeof(LoginPage));


        //if (Resources.TryGetValue("FlyoutHeaderKey", out var value))
        //    FlyoutHeader = value;
#if WINDOWS

        if (Resources.TryGetValue("FlyoutHeaderKey", out var value))
            FlyoutHeader = value;

#elif MACCATALYST
        
        if (Resources.TryGetValue("ShellItemTemplateKey", out var value1))
        {
            if (value1 is DataTemplate dataTemplate)
                ItemTemplate = dataTemplate;
        }

#endif

    }

    private async void Shell_Loaded(object sender, EventArgs e)
    {
        //var titleBarView = GetTitleView(this);

        //titleBarView.Background = Colors.Red;

        //FlyoutIcon = 

        await GoToAsync("LoginRouter", true);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);

        if (args is null)
            return;

        var location = args.Target.Location;
        if (location == null)
            return;

        if (location.OriginalString.Contains("LoginRouter"))
            FlyoutBehavior = FlyoutBehavior.Disabled;
        else
        {
            if (FlyoutBehavior != FlyoutBehavior.Locked)
                FlyoutBehavior = FlyoutBehavior.Locked;
        }

    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);
    }

    protected override bool OnBackButtonPressed()
    {
        //var currentItem = CurrentItem;
        //var shell = this;

        if (CurrentPage.Title == "LoginPage")
            return false;

        return base.OnBackButtonPressed();
    }
}