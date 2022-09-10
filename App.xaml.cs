using Microsoft.Maui.ApplicationModel;

namespace QueueSystem;

public partial class App : Application
{
	public App()
	{
        InitializeComponent();
        MainPage = new AppShell();
	}
}
