namespace MauiAppZx.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
    }
}