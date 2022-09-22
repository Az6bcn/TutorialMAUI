using TutorialMAUI.View;

namespace TutorialMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register route
		Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}
}
