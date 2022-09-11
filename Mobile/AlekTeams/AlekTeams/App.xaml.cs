using AlekTeams.Pages;

namespace AlekTeams;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainPage());
    }
}
