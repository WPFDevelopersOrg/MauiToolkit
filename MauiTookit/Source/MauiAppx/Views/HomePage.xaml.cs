namespace MauiAppx.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("LoginRouter");  
    }

    private void SearchBar_Loaded(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        //if (Application.Current is not null)
        //    Application.Current.MainPage = new AppShellx();

    }
}