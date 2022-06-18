namespace MauiAppx.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (Application.Current is null)
            return;

        Application.Current.MainPage = new AppShellx();
    }

    private void SearchBar_Loaded(object sender, EventArgs e)
    {

    }
}