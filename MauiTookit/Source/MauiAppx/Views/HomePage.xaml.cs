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
}