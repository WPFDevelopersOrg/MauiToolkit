namespace MauiAppx.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
        Window window = new Window(new HomePage());
       

        Application.Current?.OpenWindow(window);
	}
}